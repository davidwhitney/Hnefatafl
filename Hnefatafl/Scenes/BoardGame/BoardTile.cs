using Hnefatafl.Fx;
using Microsoft.Xna.Framework;

namespace Hnefatafl.Scenes.BoardGame
{
    public class BoardTile : IGetDrawn, ISupportInput
    {
        public Rectangle Location { get; set; }
        public Piece Occupant { get; set; }

        public bool Selected { get; set; }

        public void OnSelect()
        {
            Selected = !Selected;

            if (Occupant != null)
            {
                Occupant.OnSelect();
            }
        }
    }

    public class GameBoard : Piece
    {
    }

    public class Piece : ISupportInput
    {
        public void OnSelect()
        {
        }
    }

    public class DefenderKing : Piece
    {
    }

    public class Attacker : Piece
    {
    }
}