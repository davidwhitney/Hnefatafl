using Hnefatafl.Fx;
using Microsoft.Xna.Framework;

namespace Hnefatafl.Scenes.BoardGame
{
    public class BoardTile : IGetDrawn, ISupportMouseInput
    {
        public Rectangle Location { get; set; }
        public Piece Occupant { get; set; }

        public void OnClick()
        {
            if (Occupant != null)
            {
                Occupant.OnClick();
            }
        }
    }

    public class Defender : Piece
    {
    }

    public class Piece : ISupportMouseInput
    {
        public bool Selected { get; set; }

        public void OnClick()
        {
            Selected = !Selected;
        }
    }

    public class DefenderKing : Piece
    {
    }

    public class Attacker : Piece
    {
    }
}