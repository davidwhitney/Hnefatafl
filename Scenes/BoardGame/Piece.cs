using Hnefatafl.Fx;
using Microsoft.Xna.Framework;

namespace Hnefatafl.Scenes.BoardGame
{
    public class Piece : IGetDrawn
    {
        public Rectangle Location { get; set; }
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