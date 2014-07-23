using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hnefatafl.Fx
{
    public abstract class SceneRenderer<TScene>: IRender<TScene>
    {
        protected readonly ContentManager Content;
        protected readonly GraphicsDeviceManager Graphics;

        protected SceneRenderer(ContentManager content, GraphicsDeviceManager graphics)
        {
            Content = content;
            Graphics = graphics;
        }

        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Render(TScene scene, SpriteBatch batch);
        
        public void Render(IScene scene, SpriteBatch batch)
        {
            Render((TScene)scene, batch);
        }
    }
}