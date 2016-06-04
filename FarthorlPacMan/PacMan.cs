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

        public void move(Graphics graphic,Engine engine, string direction)
        {
            if (!String.IsNullOrEmpty(direction))
            {
                movedDirection = direction;
                if (movedDirection=="Right")
                {
                    tryMoveRight(graphic, engine);
                }
            }
        }

        private void tryMoveRight(Graphics graphics, Engine engine)
        {
            
            if (positionQuandrantX < engine.GetMaxX()-1)
            {
                int nextQuandrantX = this.positionQuandrantX + 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = engine.getQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[3]=="0")
                {
                    this.clearPacMan(graphics);

                    if (elements[4]=="1")
                    {
                        eatPoints += int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.movePacMan(graphics,nextQuandrantX,nextQuadrantY,"Right");
                    this.positionQuandrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Right";
                    engine.updateMatrihElements(this.positionQuandrantX, this.positionQuadrantY, elements);

                } else if (elements[3]=="1" && positionQuandrantX < engine.GetMaxX()-1)
                {
                    movedDirection = previousDirection;
                    this.move(graphics, engine,movedDirection);
                }
            }
        }

        private void tryMoveLeft(Graphics graphics, Engine engine)
        {

            if (positionQuandrantX > 1)
            {
                int nextQuandrantX = this.positionQuandrantX - 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = engine.getQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[1] == "0")
                {
                    this.clearPacMan(graphics);

                    if (elements[4] == "1")
                    {
                        eatPoints += int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.movePacMan(graphics, nextQuandrantX, nextQuadrantY, "Left");
                    this.positionQuandrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Left";
                    engine.updateMatrihElements(this.positionQuandrantX, this.positionQuadrantY, elements);

                }
                else if (elements[1] == "1" && positionQuandrantX > 0)
                {
                    movedDirection = previousDirection;
                    this.move(graphics, engine, movedDirection);
                }
            }
        }

        private void clearPacMan(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color.Black), ((positionQuandrantX*50)+25 - (diameter / 2) - 1),
                   ((positionQuadrantY*50)+25 - (diameter / 2) - 1), diameter + 2, diameter + 2);
        }

        public void drawPacMan(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(pacManColor), (positionQuandrantX*50)+25 - (diameter / 2),
                ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);
        }

        async public void movePacMan(Graphics graphics, int nextX, int nextY, string moving)
        {
            switch (moving)
            {
                case "Right":
                    for (int x = (positionQuandrantX * 50) + 25; x < (nextX*50)+25; x++)
                    {                
                        graphics.FillEllipse(new SolidBrush(Color.Black), x-1 - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        graphics.FillEllipse(new SolidBrush(pacManColor), x - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        System.Threading.Thread.Sleep(80);
                    }
                    break;

                case "Left":
                    for (int x = (positionQuandrantX * 50) + 25; x > (nextX * 50) + 25; x--)
                    {
                        graphics.FillEllipse(new SolidBrush(pacManColor), x+1 - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        graphics.FillEllipse(new SolidBrush(pacManColor), x - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        System.Threading.Thread.Sleep(80);
                    }
                    break;

                case "Up":
                    for (int y = (positionQuadrantY * 50) + 25; y > (nextY * 50) + 25; y--)
                    {
                        graphics.FillEllipse(new SolidBrush(pacManColor), y+1 - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        graphics.FillEllipse(new SolidBrush(pacManColor), y - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        System.Threading.Thread.Sleep(80);
                    }
                    break;

                case "Down":
                    for (int y = (positionQuadrantY * 50) + 25; y < (nextY * 50) + 25; y++)
                    {
                        graphics.FillEllipse(new SolidBrush(pacManColor), y+1 - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        graphics.FillEllipse(new SolidBrush(pacManColor), y - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        System.Threading.Thread.Sleep(80);
                    }
                    break;
            }
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
