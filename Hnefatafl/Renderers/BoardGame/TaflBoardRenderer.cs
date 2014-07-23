using Hnefatafl.Fx;
using Hnefatafl.Scenes.BoardGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hnefatafl.Renderers.BoardGame
{
    public class TaflBoardRenderer : SceneRenderer<BoardGameScene>
    {
        private Texture2D _piece;

        public TaflBoardRenderer(ContentManager content, GraphicsDeviceManager graphics):base(content, graphics)
        {
        }

        public override void LoadContent()
        {
            _piece = Content.Load<Texture2D>("Piece");
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public override void Render(BoardGameScene scene, SpriteBatch batch)
        {
            const int borderOffset = 50;
            const int pieceSize = 45;
            const int scale = 1;
            const int scaledPieceSize = pieceSize * scale;

            foreach (var boardTile in scene.GameBoard.Tiles)
            {
                var drawPosX = (boardTile.X * scaledPieceSize) + borderOffset;
                var drawPosY = (boardTile.Y * scaledPieceSize) + borderOffset;

                Color colour;
                if (boardTile.Occupant is DefenderKing)
                {
                    colour = Color.BlanchedAlmond;
                }
                else if (boardTile.Occupant is Defender)
                {
                    colour = Color.White;
                }
                else if (boardTile.Occupant is Attacker)
                {
                    colour = Color.Black;
                }
                else
                {
                    colour = Color.Brown;
                }

                if (boardTile.Occupant != null && boardTile.Selected)
                {
                    colour = Color.Red;
                }

                var loc = new Rectangle(drawPosX, drawPosY, scaledPieceSize, scaledPieceSize);
                batch.Draw(_piece, loc, colour);

                boardTile.Location = loc;
            }
        }
    }
}