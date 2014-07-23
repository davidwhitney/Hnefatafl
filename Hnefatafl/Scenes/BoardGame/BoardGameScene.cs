using Hnefatafl.Fx;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hnefatafl.Scenes.BoardGame
{
    public class BoardGameScene : IScene<BoardGameScene>
    {
        public IRender<BoardGameScene> Renderer { get; private set; }
        IRender IScene.Renderer { get { return Renderer; } }

        public TaflBoard GameBoard { get; private set; }

        private IController _mc;

        public BoardGameScene(Game game, IRender<BoardGameScene> renderer)
        {
            Renderer = renderer;
            game.IsMouseVisible = true;
        }

        public void Initialize()
        {
            GameBoard = new TaflBoard();

            _mc = new MouseInputController
            {
                OnLeftClick = Select
            };
        }
        
        public object Update(GameTime gameTime)
        {
            _mc.ReadInput();
            return null;
        }

        private object _selectedItem;
        private void Select(Coords coords)
        {
            var thisItem = FindClickedDrawable(coords);
            _selectedItem = thisItem;

            if (_selectedItem is ISupportInput)
            {
                (_selectedItem as ISupportInput).OnSelect();
            }
        }

        private IGetDrawn FindClickedDrawable(Coords coords)
        {
            for (var x = 0; x < GameBoard.Positions.GetLength(0); x++)
            for (var y = 0; y < GameBoard.Positions.GetLength(1); y++)
            {
                var piece = GameBoard.Positions[x, y];
                if (piece == null)
                {
                    continue;
                }

                if (coords.X >= piece.Location.X && coords.X <= piece.Location.X + piece.Location.Width
                    && coords.Y >= piece.Location.Y && coords.Y <= piece.Location.Y + piece.Location.Height)
                {
                    return piece;
                }
            }

            return null;
        }
    }
}