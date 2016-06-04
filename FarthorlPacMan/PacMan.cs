using System;
using System.Drawing;

namespace FarthorlPacMan
{
    class PacMan
    {
        private Boolean isAlive = true;
        private int positionQuandrantX = 0;
        private int positionQuadrantY = 0;
        private const int diameter = 30;
        private Color pacManColor = Color.Yellow;
        private string movedDirection = "";
        private string previousDirection = "";
        private int eatPoints = 0;

        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphics,Engine engine)
        {
            this.positionQuandrantX = positionXQaundarnt;
            this.positionQuadrantY = positionYQuadrant;
            this.initializePacMan(graphics,engine);
        }

        public void move(Graphics graphic,Engine engine)
        {
            if (!String.IsNullOrEmpty(movedDirection))
            {
                if (movedDirection=="Right")
                {
                    tryMoveRight(graphic, engine);
                }
            }
        }

        private void tryMoveRight(Graphics graphics, Engine engine)
        {
            
            if (positionQuandrantX < engine.GetMaxX())
            {
                int nextQuandrantX = this.positionQuandrantX + 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = engine.getQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[1]=="0")
                {
                    this.clearPacMan(graphics);

                    if (elements[4]=="1")
                    {
                        eatPoints += int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.positionQuandrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    this.drawPacMan(graphics);
                    previousDirection = "Right";
                    engine.updateMatrihElements(this.positionQuandrantX, this.positionQuadrantY, elements);

                } else if (elements[1]=="1")
                {
                    movedDirection = previousDirection;
                    this.move(graphics, engine);
                }
            }
        }

        private void clearPacMan(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color.Black), (positionQuandrantX*50 - (diameter / 2) - 1),
                   (positionQuadrantY - (diameter / 2) - 1), diameter + 2, diameter + 2);
        }

        private void drawPacMan(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(pacManColor), ((positionQuandrantX * 50)+25 - (diameter / 2)),
                   ((positionQuadrantY*50) + 25 - (diameter / 2)), diameter, diameter);
        }

        private void initializePacMan(Graphics graphics,Engine engine)
        {
            string[] elements = engine.getQuadrantElements(this.positionQuandrantX, this.positionQuadrantY);

          
                if (elements[4] == "1")
                {
                    eatPoints += int.Parse(elements[4]);
                    elements[4] = "0";

                }
                this.drawPacMan(graphics);
                engine.updateMatrihElements(this.positionQuandrantX, this.positionQuadrantY, elements);

        }

    }
}
