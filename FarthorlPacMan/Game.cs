namespace FarthorlPacMan
{
    using System.Drawing;

    class Game
    {
        private Engine graphicEngine;
        public void startDraw(Graphics graphic, GameWindows game)
        {
            this.graphicEngine = new Engine(graphic, game);
            this.graphicEngine.Initialize();
        }

        public void stopGame()
        {
            this.graphicEngine.stopGame();
        }

        public void Direction(string direction)
        {
            graphicEngine.changeDirection(direction);
        }

        public void pauseGame()
        {
            graphicEngine.pauseGame();
        }
    }
}
