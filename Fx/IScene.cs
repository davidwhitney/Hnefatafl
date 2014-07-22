using Microsoft.Xna.Framework;

namespace Hnefatafl.Fx
{
    public interface IScene<in TScene> : IScene
    {
        new IRender<TScene> Renderer { get; }
    }

    public interface IScene
    {
        IRender Renderer { get; }
        void Initialize();
        object Update(GameTime gameTime);
    }
}