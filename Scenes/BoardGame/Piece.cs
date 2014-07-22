using Hnefatafl.Fx;
using Microsoft.Xna.Framework;

namespace Hnefatafl.Scenes.BoardGame
{
    public class Piece : IGetDrawn, ISupportMouseInput
    {
        public Rectangle Location { get; set; }
        public bool Selected { get; set; }

        public void OnClick()
        {
            
        }
    }

    public class Defender : Piece
    {
    }

    public class DefenderKing : Piece
    {
    }

    public class Attacker : Piece
    {
    }
}