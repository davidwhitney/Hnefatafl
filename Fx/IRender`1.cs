using Microsoft.Xna.Framework.Graphics;

namespace Hnefatafl.Fx
{
    public interface IRender<in TScene> : IRender
    {
        void Render(TScene scene, SpriteBatch batch);
    }
}