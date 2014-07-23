using System.Collections.Generic;
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

            for (var x = 0; x < scene.GameBoard.Positions.GetLength(0); x++)
            for (var y = 0; y < scene.GameBoard.Positions.GetLength(1); y++)
            {
                var boardTile = scene.GameBoard.Positions[x, y];
                var scaledPieceSize = pieceSize*scale;

                var drawPosX = (x*scaledPieceSize) + borderOffset;
                var drawPosY = (y*scaledPieceSize) + borderOffset;

                Color colour;
                if (boardTile.Occupant is GameBoard)
                {
                    colour = Color.White;
                }
                else if (boardTile.Occupant is DefenderKing)
                {
                    colour = Color.BlanchedAlmond;
                }
                else if (boardTile.Occupant is Attacker)
                {
                    colour = Color.Black;
                }
                else
                {
                    colour = Color.Brown;
                }

                if (boardTile.Occupant != null && boardTile.Occupant.Selected)
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