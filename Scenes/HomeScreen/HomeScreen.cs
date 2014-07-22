using System;
using System.Collections.Generic;
using Hnefatafl.Fx;
using Microsoft.Xna.Framework;

namespace Hnefatafl.Scenes.HomeScreen
{
    public class HoneScene : IScene<HoneScene>
    {
        IRender IScene.Renderer { get { return Renderer; } }
        public IRender<HoneScene> Renderer { get; private set; }

        public void Initialize()
        {
        }

        public object Update(GameTime gameTime)
        {
            return null;
        }
    }
}
