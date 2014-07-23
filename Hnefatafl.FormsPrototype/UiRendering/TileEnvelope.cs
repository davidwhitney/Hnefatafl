using Hnefatafl.GameCore;
using Hnefatafl.Scenes.BoardGame;

namespace Hnefatafl.FormsPrototype.UiRendering
{
    public class TileEnvelope
    {
        protected bool Equals(TileEnvelope other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public int X { get; set; }
        public int Y { get; set; }
        public BoardTile BoardTile { get; set; }

        public TileEnvelope(int x, int y, BoardTile boardTile)
        {
            X = x;
            Y = y;
            BoardTile = boardTile;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TileEnvelope) obj);
        }
    }
}