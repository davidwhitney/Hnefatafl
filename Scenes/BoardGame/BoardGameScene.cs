using System;
using Hnefatafl.Fx;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hnefatafl.Scenes.BoardGame
{
    public class BoardGameScene : IScene
    {
        private readonly Game _game;
        public TaflBoard GameBoard;
        private MouseInputController _mc;

        public BoardGameScene(Game game)
        {
            _game = game;
            _game.IsMouseVisible = true;
        }

        public void Initialize()
        {
            GameBoard = new TaflBoard();

            _mc = new MouseInputController(OnLeftClick);
        }


        public object Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.Exit();
            
            _mc.ReadInput();
            return null;
        }

        private void OnLeftClick(MouseState mouseState)
        {
            var drawable = FindClickedDrawable(mouseState);
            if (drawable is Piece)
            {
                var piece = drawable as Piece;
                piece.Selected = !piece.Selected;
            }
        }

        private IGetDrawn FindClickedDrawable(MouseState ms)
        {
            for (var x = 0; x < GameBoard.Positions.GetLength(0); x++)
            for (var y = 0; y < GameBoard.Positions.GetLength(1); y++)
            {
                var piece = GameBoard.Positions[x, y];
                if (piece == null)
                {
                    continue;
                }

                if (ms.X >= piece.Location.X && ms.X <= piece.Location.X + piece.Location.Width
                    && ms.Y >= piece.Location.Y && ms.Y <= piece.Location.Y + piece.Location.Height)
                {
                    return piece;
                }
            }

            return null;
        }
    }

    public class MouseInputController
    {
        private readonly Action<MouseState> _onLeftClick;

        private MouseState _previousState;

        public MouseInputController(Action<MouseState> onLeftClick)
        {
            _onLeftClick = onLeftClick ?? (ms => { });
        }

        public void ReadInput()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Released && _previousState.LeftButton == ButtonState.Pressed)
            {
                _onLeftClick(mouseState);
            }

            _previousState = mouseState;
        }
    }
}