using Hnefatafl.Fx;
using Microsoft.Xna.Framework;

namespace Hnefatafl.Scenes.BoardGame
{
    public class BoardTile : IGetDrawn, ISupportInput
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BoardTile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Rectangle Location { get; set; }
        public Piece Occupant { get; set; }

        public bool Selected { get; set; }

        public bool IsOccupied { get { return Occupant != null; } }

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
                return false;
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
    }

    public class Defender : Piece
    {
    }

    public class Piece : ISupportInput
    {
        public void OnSelect()
        {
        }
    }

    public class DefenderKing : Defender
    {
    }

    public class Attacker : Piece
    {
    }
}