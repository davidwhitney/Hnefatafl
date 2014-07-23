using System;
using System.Collections.Generic;
using Hnefatafl.Fx;
using Microsoft.Xna.Framework;

namespace Hnefatafl.GameCore
{
    public class BoardTile : IGetDrawn, ISupportInput
    {
        public int X { get; set; }
        public int Y { get; set; }

        public TileType TileType { get; set; }
        public Rectangle Location { get; set; }
        public Piece Occupant { get; set; }

        public bool Selected { get; set; }
        public bool IsOccupied { get { return Occupant != null; } }

        private readonly Dictionary<TileType, List<Type>> _occupationRules = new Dictionary<TileType, List<Type>>
        {
            {TileType.AttackerTerritory, new List<Type> { typeof(Attacker), typeof(Defender) }},
            {TileType.Castle, new List<Type> { typeof(DefenderKing) }},
            {TileType.Neutral, new List<Type> { typeof(Attacker), typeof(DefenderKing), typeof(Defender) }},
        }; 

        public BoardTile(int x, int y, TileType tileType)
        {
            X = x;
            Y = y;
            TileType = tileType;
        }

        public void OnSelect()
        {
            Selected = !Selected;

            if (Occupant != null)
            {
                Occupant.OnSelect();
            }
        }
        
        public bool OccupantIsFriendly(Piece occupant)
        {
            if (!IsOccupied)
            {
                return true;
            }

            if (Occupant.GetType() == occupant.GetType())
            {
                return true;
            }

            if (Occupant.GetType().IsInstanceOfType(occupant)
                || occupant.GetType().IsInstanceOfType(Occupant))
            {
                return true;
            }

            return false;
        }

        public bool CanOccupy(Piece occupant)
        {
            return _occupationRules[TileType].Contains(occupant.GetType());
        }
    }
}