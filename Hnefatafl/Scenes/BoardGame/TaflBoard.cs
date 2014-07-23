using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hnefatafl.Scenes.BoardGame
{
    public class TaflBoard
    {
        public BoardTile[,] Positions { get; set; }

        public TaflBoard()
        {
            var boardTemplate = new List<string>
            {
                {"0 0 0 A A A 0 0 0"},
                {"0 0 0 0 A 0 0 0 0"},
                {"0 0 0 0 D 0 0 0 0"},
                {"A 0 0 0 D 0 0 0 A"},
                {"A A D D K D D A A"},
                {"A 0 0 0 D 0 0 0 A"},
                {"0 0 0 0 D 0 0 0 0"},
                {"0 0 0 0 A 0 0 0 0"},
                {"0 0 0 A A A 0 0 0"},
            };

            Positions = new BoardTile[boardTemplate[0].Split().Length, boardTemplate.Count];
            for (var x = 0; x < boardTemplate.Count; x++)
            {
                var items = boardTemplate[x].Split();
                for (var y = 0; y < items.Length; y++)
                {
                    var item = items[y];
                    var tile = new BoardTile(x, y);
                    switch (item)
                    {
                        case "A":
                            tile.Occupant = new Attacker();
                            break;
                        case "D":
                            tile.Occupant = new Defender();
                            break;
                        case "K":
                            tile.Occupant = new DefenderKing();
                            break;
                    }
                    Positions[x, y] = tile;
                }
            }
        }

        public void SelectTile(BoardTile selectedTile)
        {
            var lastSelectedTile = Tiles.FirstOrDefault(t => t.Selected) ?? new BoardTile(-1, -1);
            selectedTile.OnSelect();

            if (UserAttemptedToMoveOccupant(lastSelectedTile, selectedTile) 
                && OccupantMovementIsLegal(lastSelectedTile, selectedTile))
            {
                MoveOccupant(lastSelectedTile, selectedTile);

                CheckForVictories(selectedTile);
                selectedTile.Selected = false;
            }

            lastSelectedTile.Selected = false;
        }

        private void CheckForVics()
        {

        }

        private void CheckForVictories(BoardTile selectedTile)
        {
            var neighbours = new Neighbours
            {
                X1 = TryGetTile(selectedTile.X + 1, selectedTile.Y),
                X2 = TryGetTile(selectedTile.X + 2, selectedTile.Y),
                XMinus1 = TryGetTile(selectedTile.X - 1, selectedTile.Y),
                XMinus2 = TryGetTile(selectedTile.X - 2, selectedTile.Y),
                Y1 = TryGetTile(selectedTile.X, selectedTile.Y + 1),
                Y2 = TryGetTile(selectedTile.X, selectedTile.Y + 2),
                YMinus1 = TryGetTile(selectedTile.X, selectedTile.Y - 1),
                YMinus2 = TryGetTile(selectedTile.X, selectedTile.Y - 2),
            };

            if (!neighbours.X1.OccupantIsFriendly(selectedTile.Occupant)
                && neighbours.X2.OccupantIsFriendly(selectedTile.Occupant))
            {
                neighbours.X1.Occupant = null;
            }

            if (!neighbours.XMinus1.OccupantIsFriendly(selectedTile.Occupant)
                && neighbours.XMinus2.OccupantIsFriendly(selectedTile.Occupant))
            {
                neighbours.XMinus1.Occupant = null;
            }

            if (!neighbours.Y1.OccupantIsFriendly(selectedTile.Occupant)
                && neighbours.Y2.OccupantIsFriendly(selectedTile.Occupant))
            {
                neighbours.Y1.Occupant = null;
            }

            if (!neighbours.YMinus1.OccupantIsFriendly(selectedTile.Occupant)
                && neighbours.YMinus2.OccupantIsFriendly(selectedTile.Occupant))
            {
                neighbours.YMinus1.Occupant = null;
            }
        }

        private class Neighbours
        {
            public BoardTile X1 { get; set; }
            public BoardTile X2 { get; set; }
            public BoardTile Y1 { get; set; }
            public BoardTile Y2 { get; set; }
            public BoardTile XMinus1 { get; set; }
            public BoardTile XMinus2 { get; set; }
            public BoardTile YMinus1 { get; set; }
            public BoardTile YMinus2 { get; set; }
        }

        private BoardTile TryGetTile(int x, int y)
        {
            try { return Positions[x, y]; }
            catch { return new BoardTile(-1, -1); }
        }

        private static bool UserAttemptedToMoveOccupant(BoardTile @from, BoardTile to)
        {
            return @from.IsOccupied && !to.IsOccupied;
        }

        private bool OccupantMovementIsLegal(BoardTile @from, BoardTile to)
        {
            if (@from.X == to.X && CheckPathIsClear(@from, to, checkAll => checkAll.Y, y => Positions[to.X, y]))
            {
                return true;
            }

            if (@from.Y == to.Y && CheckPathIsClear(@from, to, checkAll => checkAll.X, x => Positions[x, to.Y]))
            {
                return true;
            }

            return false;
        }

        private static bool CheckPathIsClear(BoardTile lastSelectedTile, BoardTile selectedTile, Expression<Func<BoardTile, int>> distanceBetween, Func<int, BoardTile> getCell)
        {
            var getDiscriminatingProperty = distanceBetween.Compile();

            var smallestDimension = selectedTile;
            var largestDimension = lastSelectedTile;

            if (getDiscriminatingProperty(lastSelectedTile) < getDiscriminatingProperty(selectedTile))
            {
                smallestDimension = lastSelectedTile;
                largestDimension = selectedTile;
            }

            for (var iterator = getDiscriminatingProperty(smallestDimension) + 1; iterator < getDiscriminatingProperty(largestDimension); iterator++)
            {
                var cell = getCell(iterator);
                if (cell.IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }

        private static void MoveOccupant(BoardTile @from, BoardTile to)
        {
            to.Occupant = @from.Occupant;
            @from.Occupant = null;
        }

        public IEnumerable<BoardTile> Tiles
        {
            get
            {
                for (var x = 0; x < Positions.GetLength(0); x++)
                for (var y = 0; y < Positions.GetLength(1); y++)
                {
                    yield return Positions[x, y];
                }
            }
        }
    }
}