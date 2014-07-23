using Hnefatafl.Fx;
using Microsoft.Xna.Framework;

namespace Hnefatafl.Scenes.HomeScreen
{
    public class HomeScene : IScene<HomeScene>
    {
        public Game Game { get; set; }

        public HomeScene(Game game, IRender<HomeScene> homeSceneRenderer)
        {
            Game = game;
            Renderer = homeSceneRenderer;
        }

        IRender IScene.Renderer { get { return Renderer; } }
        public IRender<HomeScene> Renderer { get; private set; }

        public void Initialize()
        {
        }

        public object Update(GameTime gameTime)
        {
            return null;
        }
    }
}
