using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hnefatafl.FormsPrototype.UiRendering;
using Hnefatafl.Scenes.BoardGame;

namespace Hnefatafl.FormsPrototype
{
    public partial class Form1 : Form
    {
        private TaflBoard _boardData;
        private TaflBoardWinformsRenderer _renderer;

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
            _renderer = new TaflBoardWinformsRenderer();
            panel1.Controls.Clear();
            panel1.Visible = false;

            _renderer.Render(_boardData, panel1);
            _renderer.TileClicked = OnTileClick;

            panel1.Visible = true;
        }

        private void OnTileClick(TileEnvelope loc, Panel uiControl)
        {
            _boardData.SelectTile(loc.BoardTile);
            _renderer.Render(_boardData, panel1);
        }
    }
}
