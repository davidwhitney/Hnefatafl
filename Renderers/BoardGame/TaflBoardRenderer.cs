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
        const int PieceSize = 45;

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
            for (var x = 0; x < scene.GameBoard.Positions.GetLength(0); x++)
            {
                for (var y = 0; y < scene.GameBoard.Positions.GetLength(1); y++)
                {
                    var row = scene.GameBoard.Positions[x, y];

                    var drawPosX = (x * PieceSize) + 20;
                    var drawPosY = (y * PieceSize) + 20;
                    
                    Color colour;
                    if (row is Defender)
                    {
                        colour = Color.White;
                    }
                    else if (row is DefenderKing)
                    {
                        colour = Color.BlanchedAlmond;
                    }
                    else if (row is Attacker)
                    {
                        colour = Color.Black;
                    }
                    else
                    {
                        colour = Color.Brown;
                    }

                    var loc = new Rectangle(drawPosX, drawPosY, PieceSize, PieceSize);
                    batch.Draw(_piece, loc, colour);

                    row.Location = loc;
                }
            }
        }
    }
}