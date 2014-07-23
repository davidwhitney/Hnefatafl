using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;

namespace Hnefatafl.Scenes.BoardGame
{
    public class TaflBoard
    {
        public BoardTile[,] Positions { get; set; }
        public Type Victor { get; set; }

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
                var moveResult = CheckForVictories(selectedTile);

                if (moveResult.HasValue && moveResult.Value == Outcome.KingDefeated)
                {
                    Victor = typeof (Attacker);
                }

                selectedTile.Selected = false;
            }

            lastSelectedTile.Selected = false;
        }

        private Outcome? CheckForVictories(BoardTile selectedTile)
        {
            foreach (var tile in Tiles)
            {
                if (tile.IsOccupied)
                {
                    var neighbours = new Neighbours
                    {
                        X1 = TryGetTile(tile.X + 1, tile.Y),
                        XMinus1 = TryGetTile(tile.X - 1, tile.Y),
                        Y1 = TryGetTile(tile.X, tile.Y + 1),
                        YMinus1 = TryGetTile(tile.X, tile.Y - 1),
                    };

                    var xUnfriendly = false;
                    var yUnfriendly = false;

                    if (!neighbours.X1.OccupantIsFriendly(tile.Occupant)
                        && !neighbours.XMinus1.OccupantIsFriendly(tile.Occupant))
                    {
                        xUnfriendly = true;
                    }  
                    
                    if (!neighbours.Y1.OccupantIsFriendly(tile.Occupant)
                        && !neighbours.YMinus1.OccupantIsFriendly(tile.Occupant))
                    {
                        yUnfriendly = true;
                    }

                    if (tile.Occupant is DefenderKing
                        && xUnfriendly
                        && yUnfriendly)
                    {
                        return Outcome.KingDefeated;
                    }

                    if ((tile.Occupant is Attacker || (tile.Occupant is Defender && !(tile.Occupant is DefenderKing)))
                        && (xUnfriendly || yUnfriendly))
                    {
                        //capture piece
                        tile.Occupant = null;
                        return Outcome.PieceTaken;
                    }
                }
            }

            return null;
        }

        private enum Outcome
        {
            KingDefeated,
            PieceTaken,
        }

        private class Neighbours
        {
            public BoardTile X1 { get; set; }
            public BoardTile Y1 { get; set; }
            public BoardTile XMinus1 { get; set; }
            public BoardTile YMinus1 { get; set; }
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