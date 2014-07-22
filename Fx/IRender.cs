using System.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace Hnefatafl.Fx
{
    public interface IRender
    {
        void LoadContent();
        void UnloadContent();
        void Render(IScene scene, SpriteBatch batch);
    }
}