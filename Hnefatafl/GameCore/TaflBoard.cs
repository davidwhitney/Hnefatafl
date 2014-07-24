using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hnefatafl.GameCore
{
    public class TaflBoard
    {
        public BoardTile[,] Positions { get; set; }
        
        public Type Victor { get; set; }
        public int PossibleEscapeVectors { get; private set; }

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
                    var tile = new BoardTile(x, y, TileType.Neutral);
                    switch (item)
                    {
                        case "A":
                            tile.Occupant = new Attacker();
                            tile.TileType = TileType.AttackerTerritory;
                            break;
                        case "D":
                            tile.Occupant = new Defender();
                            break;
                        case "K":
                            tile.Occupant = new DefenderKing();
                            tile.TileType = TileType.Castle;
                            break;
                    }
                    Positions[x, y] = tile;
                }
            }
        }

        public void SelectTile(BoardTile selectedTile)
        {
            var lastSelectedTile = Tiles.FirstOrDefault(t => t.Selected) ?? new BoardTile(-1, -1, TileType.Neutral);
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

                var kingsTile = Tiles.Single(x => x.Occupant is DefenderKing);
                PossibleEscapeVectors = KingEscapeVectors(kingsTile);
                if (PossibleEscapeVectors > 1 || kingsTile.X == 0 || kingsTile.X == 8 || kingsTile.Y == 0 || kingsTile.Y == 8)
                {
                    Victor = typeof (DefenderKing);
                }

                selectedTile.Selected = false;
            }

            lastSelectedTile.Selected = false;
        }

        private int KingEscapeVectors(BoardTile kingsTile)
        {
            return new List<BoardTile>
            {
                Positions[kingsTile.X, 0],
                Positions[kingsTile.X, 8],
                Positions[0, kingsTile.Y],
                Positions[8, kingsTile.Y]
            }.Count(escape => OccupantMovementIsLegal(kingsTile, escape));
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

        private BoardTile TryGetTile(int x, int y)
        {
            if (x < 0 || x > 8 || y < 0 || y > 8)
            {
                return new BoardTile(-1, -1, TileType.Neutral);
            }

            try { return Positions[x, y]; }
            catch { return new BoardTile(-1, -1, TileType.Neutral); }
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

        private static bool CheckPathIsClear(BoardTile @from, BoardTile destination, Expression<Func<BoardTile, int>> distanceBetween, Func<int, BoardTile> getCell)
        {
            var getDiscriminatingProperty = distanceBetween.Compile();

            var smallestDimension = destination;
            var largestDimension = @from;

            if (getDiscriminatingProperty(@from) < getDiscriminatingProperty(destination))
            {
                smallestDimension = @from;
                largestDimension = destination;
            }

            for (var iterator = getDiscriminatingProperty(smallestDimension) + 1; iterator < getDiscriminatingProperty(largestDimension); iterator++)
            {
                var cell = getCell(iterator);
                if (cell.IsOccupied)
                {
                    return false;
                }

                if (!cell.CanOccupy(@from.Occupant))
                {
                    return false;
                }
            }

            if (destination.IsOccupied || !destination.CanOccupy(@from.Occupant))
            {
                return false;
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