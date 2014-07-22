using Hnefatafl.Fx;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hnefatafl.Scenes.BoardGame
{
    public class BoardGameScene : IScene
    {
        private readonly Game _game;
        public TaflBoard GameBoard;

        public BoardGameScene(Game game)
        {
            _game = game;
            _game.IsMouseVisible = true;
        }

        public void Initialize()
        {
            GameBoard = new TaflBoard();
        }

        public object Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.Exit();

            return null;
        }
    }
}