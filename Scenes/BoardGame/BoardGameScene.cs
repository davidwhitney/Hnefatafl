using System.Collections.Generic;
using Hnefatafl.Fx;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hnefatafl.Scenes.BoardGame
{
    public class BoardGameScene : IScene<BoardGameScene>, ICanBeRendered
    {
        public IRender<BoardGameScene> Renderer { get; private set; }
        IRender IScene.Renderer { get { return Renderer; } }

        public TaflBoard GameBoard { get; private set; }

        private readonly Game _game;
        private MouseInputController _mc;

        public BoardGameScene(Game game, IRender<BoardGameScene> renderer)
        {
            _game = game;
            Renderer = renderer;
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
            if (drawable is ISupportMouseInput)
            {
                (drawable as ISupportMouseInput).OnClick();
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

        public IList<IGetDrawn> GetDrawables()
        {
            return new IGetDrawn[0];
        }
    }
}