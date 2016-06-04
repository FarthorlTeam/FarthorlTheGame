namespace FarthorlPacMan
{
    using System.Drawing;
    using System.Windows.Forms;
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
