using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Hnefatafl.Fx;
using Hnefatafl.Renderers.BoardGame;
using Hnefatafl.Scenes.BoardGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Hnefatafl
{
    public class GameStart : Game
    {
        SpriteBatch _spriteBatch;

        private readonly Dictionary<IScene, IRender> _scenes;
        public KeyValuePair<IScene, IRender> ActiveScene { get { return _scenes.First(); } } 

        public GameStart()
        {
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _scenes = new Dictionary<IScene, IRender>
            {
                {new BoardGameScene(this), new TaflBoardRenderer(Content, graphics)}
            };
        }

        protected override void Initialize()
        {
            foreach (var scene in _scenes)
            {
                scene.Key.Initialize();;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (var scene in _scenes)
            {
                scene.Value.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            foreach (var scene in _scenes)
            {
                scene.Value.UnloadContent();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            ActiveScene.Key.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

            _spriteBatch.Begin();
            ActiveScene.Value.Render(ActiveScene.Key, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
