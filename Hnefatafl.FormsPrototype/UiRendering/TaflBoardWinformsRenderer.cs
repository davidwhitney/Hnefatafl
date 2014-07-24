using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Hnefatafl.GameCore;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;

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

        public void Render(TaflBoard gameBoard, Panel targetPanel, Label messages)
        {
            if (gameBoard.PossibleEscapeVectors == 1)
            {
                messages.Text = "Raichi";
            }
            else
            {
                messages.Text = "";
            }
            
            const int borderOffset = 0;
            const int pieceSize = 45;
            const int scale = 1;
            const int scaledPieceSize = pieceSize * scale;

            foreach (var boardTile in gameBoard.Tiles)
            {
                var thisLocation = new TileEnvelope(boardTile.X, boardTile.Y, boardTile);
                var drawPosX = (boardTile.Y * scaledPieceSize) + borderOffset;
                var drawPosY = (boardTile.X * scaledPieceSize) + borderOffset;

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

            if (gameBoard.Victor != null)
            {
                targetPanel.Enabled = false;
                messages.Text = "Game over " + gameBoard.Victor.Name + " wins.";
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

            if (boardTile.IsOccupied)
            {
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
                    colour = Color.LightGreen;
                }
            }
            else
            {
                switch (boardTile.TileType)
                {
                    case TileType.Castle:
                        colour = Color.Silver;
                        break;
                    case TileType.AttackerTerritory:
                        colour = Color.DarkGreen;
                        break;
                    default:
                        colour = Color.LightGreen;
                        break;
                }
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