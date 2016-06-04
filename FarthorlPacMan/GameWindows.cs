using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarthorlPacMan
{
    public partial class GameWindows : Form
    {
        private Game game=new Game();
        public GameWindows()
        {
            InitializeComponent();
        }

        private void pacMan_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = pacMan.CreateGraphics();
            this.game.startDraw(graphics);
        }

        private void GameWindows_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.game.stopGame();
        }
    }
}
