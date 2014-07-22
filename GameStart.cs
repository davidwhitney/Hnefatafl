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

        private readonly List<IScene> _scenes;
        public IScene ActiveScene { get { return _scenes.First(); } } 

        public GameStart()
        {
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _scenes = new List<IScene>
            {
                new BoardGameScene(this, new TaflBoardRenderer(Content, graphics))
            };
        }

        protected override void Initialize()
        {
            foreach (var scene in _scenes)
            {
                scene.Initialize();;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (var scene in _scenes)
            {
                scene.Renderer.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            foreach (var scene in _scenes)
            {
                scene.Renderer.UnloadContent();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            ActiveScene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

            _spriteBatch.Begin();
            ActiveScene.Renderer.Render(ActiveScene, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
