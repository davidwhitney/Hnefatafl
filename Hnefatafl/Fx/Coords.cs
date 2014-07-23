using Microsoft.Xna.Framework.Input;

namespace Hnefatafl.Fx
{
    public class Coords
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coords(MouseState ms)
        {
            X = ms.X;
            Y = ms.Y;
        }
    }
}