using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Hnefatafl.Scenes.BoardGame;

namespace Hnefatafl.FormsPrototype.UiRendering
{
    public class TaflBoardWinformsRenderer
    {
        public Action<TileEnvelope, Panel> TileClicked { get; set; }

        private readonly Dictionary<Panel, TileEnvelope> _lookup;

        public TaflBoardWinformsRenderer()
        {
            TileClicked = (loc, ctrl) => { };
            _lookup = new Dictionary<Panel, TileEnvelope>();
        }

        public void Render(TaflBoard gameBoard, Panel targetPanel)
        {
            const int borderOffset = 0;
            const int pieceSize = 45;

            const int scale = 1;

            for (var x = 0; x < gameBoard.Positions.GetLength(0); x++)
            for (var y = 0; y < gameBoard.Positions.GetLength(1); y++)
            {
                var boardTile = gameBoard.Positions[x, y];
                var thisLocation = new TileEnvelope(x, y, boardTile);

                var scaledPieceSize = pieceSize * scale;

                var drawPosX = (x * scaledPieceSize) + borderOffset;
                var drawPosY = (y * scaledPieceSize) + borderOffset;

                Panel panel;
                if (_lookup.ContainsValue(thisLocation))
                {
                    panel = _lookup.Single(_ => _.Value.Equals(thisLocation)).Key;
                }
                else
                {
                    panel = new Panel { Location = new Point(drawPosX, drawPosY) };
                    panel.Click += panel_Click;
                   
                    _lookup.Add(panel, thisLocation);
                    targetPanel.Controls.Add(panel);
                }
                panel.Width = scaledPieceSize;
                panel.Height = scaledPieceSize;

                RefreshTile(panel, boardTile);
            }
        }

        public void RefreshTile(Panel panel, BoardTile boardTile)
        {
            panel.BackColor = DetermineTileColour(boardTile);
            
            if (boardTile.Occupant != null && boardTile.Selected)
            {
                panel.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                panel.BorderStyle = BorderStyle.None;
            }
        }

        private static Color DetermineTileColour(BoardTile boardTile)
        {
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

            return colour;
        }

        private void panel_Click(object sender, EventArgs e)
        {
            var location = _lookup.Single(x => x.Key == sender).Value;
            TileClicked(location, (sender as Panel));
        }
    }
}