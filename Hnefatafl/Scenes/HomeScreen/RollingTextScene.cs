using System.Collections.Generic;
using Hnefatafl.Fx;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hnefatafl.Scenes.HomeScreen
{
    public class RollingTextScene : IScene<RollingTextScene>, IRender<RollingTextScene>
    {
        IRender IScene.Renderer { get { return this; } }
        public IRender<RollingTextScene> Renderer { get { return this; } }

        List<FullScreenMessage> Messages { get; set; }

        public RollingTextScene(params FullScreenMessage[] messages)
        {
            Messages = new List<FullScreenMessage>(messages);
        }

        public void Initialize()
        {
        }

        public object Update(GameTime gameTime)
        {
            return null;
        }

        public void Render(RollingTextScene scene, SpriteBatch batch)
        {
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public void Render(IScene scene, SpriteBatch batch)
        {
        }
    }
}