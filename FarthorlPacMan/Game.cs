namespace FarthorlPacMan
{
    using System.Drawing;

    class Game
    {
        private Engine graphicEngine;
        public void startDraw(Graphics graphic)
        {
            this.graphicEngine = new Engine(graphic);
            this.graphicEngine.initialize();
        }

        public void stopGame()
        {
            this.graphicEngine.stopGame();
        }

        public void Direction(string direction)
        {
            graphicEngine.changeDirection(direction);
        }
    }
}
