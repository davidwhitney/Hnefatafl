using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hnefatafl.Scenes.BoardGame;

namespace Hnefatafl.FormsPrototype
{
    public partial class Form1 : Form
    {
        private TaflBoard _boardData;
        private readonly TaflBoardWinformsRenderer _renderer;

        public Form1()
        {
            InitializeComponent();
            _renderer = new TaflBoardWinformsRenderer();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create new game
            _boardData = new TaflBoard();
            _renderer.Render(_boardData, panel1);
            _renderer.TileClicked = OnTileClick;
        }

        private void OnTileClick(int x, int y)
        {

        }
    }

    public class TaflBoardWinformsRenderer
    {
        public Action<int, int> TileClicked { get; set; }

        private readonly Dictionary<Panel, Location> _lookup;

        public TaflBoardWinformsRenderer()
        {
            TileClicked = (x, y) => { };
            _lookup = new Dictionary<Panel, Location>();
        }

        public void Render(TaflBoard gameBoard, Panel targetPanel)
        {
            const int borderOffset = 0;
            const int pieceSize = 45;

            const int scale = 1;

            for (var x = 0; x < gameBoard.Positions.GetLength(0); x++)
            for (var y = 0; y < gameBoard.Positions.GetLength(1); y++)
            {
                var thisLocation = new Location(x, y);
                var boardTile = gameBoard.Positions[x, y];
                var scaledPieceSize = pieceSize * scale;

                var drawPosX = (x * scaledPieceSize) + borderOffset;
                var drawPosY = (y * scaledPieceSize) + borderOffset;

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
                panel.BackColor = colour;

                //boardTile.Location = loc;
            }
        }

        private void panel_Click(object sender, EventArgs e)
        {
            var coords = _lookup.Single(x => x.Key == sender).Value;
            TileClicked (coords.X, coords.Y);
        }

        private class Location
        {
            protected bool Equals(Location other)
            {
                return X == other.X && Y == other.Y;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (X*397) ^ Y;
                }
            }

            public int X { get; set; }
            public int Y { get; set; }

            public Location(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Location) obj);
            }
        }
    }
}
