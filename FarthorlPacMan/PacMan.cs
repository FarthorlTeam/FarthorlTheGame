using System;
using System.Drawing;

namespace FarthorlPacMan
{
    class PacMan
    {
        private Boolean isAlive = true;
        private int positionQuadrantX = 0;
        private int positionQuadrantY = 0;
        private const int diameter = 30;
        private Color pacManColor = Color.Yellow;
        private string movedDirection = "";
        private string previousDirection = "";
        private int eatPoints = 0;

        public PacMan(int positionXQaundarnt, int positionYQuadrant, Graphics graphics,Engine engine)
        {
            this.positionQuadrantX = positionXQaundarnt;
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

                if (movedDirection=="Left")
                {
                    tryMoveLeft(graphic,engine);
                }

                if (movedDirection == "Up")
                {
                    tryMoveUp(graphic, engine);
                }

                if (movedDirection == "Down")
                {
                    tryMoveDown(graphic, engine);
                }
            }
        }

        private void tryMoveRight(Graphics graphic, Engine engine)
        {
            
            if (positionQuadrantX < engine.GetMaxX()-1)
            {
                int nextQuandrantX = this.positionQuadrantX + 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = engine.getQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[3]=="0")
                {
                    this.clearPacMan(graphic);

                    if (elements[4]=="1")
                    {
                        eatPoints += int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.movePacMan(graphic, nextQuandrantX,nextQuadrantY,"Right");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Right";
                    engine.updateMatrihElements(this.positionQuadrantX, this.positionQuadrantY, elements);

                } else if (elements[3]=="1" && positionQuadrantX < engine.GetMaxX()-1)
                {
                    movedDirection = previousDirection;
                    this.move(graphic, engine,movedDirection);
                }
            }
        }

        private void tryMoveLeft(Graphics graphic, Engine engine)
        {

            if (positionQuadrantX > 0)
            {
                int nextQuandrantX = this.positionQuadrantX - 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = engine.getQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[1] == "0")
                {
                    this.clearPacMan(graphic);

                    if (elements[4] == "1")
                    {
                        eatPoints += int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Left");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Left";
                    engine.updateMatrihElements(this.positionQuadrantX, this.positionQuadrantY, elements);

                }
                else if (elements[1] == "1" && positionQuadrantX > 0)
                {
                    movedDirection = previousDirection;
                    this.move(graphic, engine, movedDirection);
                }
            }
        }

        private void tryMoveUp(Graphics graphic, Engine engine)
        {
            if (positionQuadrantY > 0)
            {
                int nextQuandrantX = this.positionQuadrantX;
                int nextQuadrantY = this.positionQuadrantY-1;
                string[] elements = engine.getQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[0] == "0")
                {
                    this.clearPacMan(graphic);

                    if (elements[4] == "1")
                    {
                        eatPoints += int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Up");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Up";
                    engine.updateMatrihElements(this.positionQuadrantX, this.positionQuadrantY, elements);

                }
                else if (elements[1] == "1" && positionQuadrantX > 0)
                {
                    movedDirection = previousDirection;
                    this.move(graphic, engine, movedDirection);
                }
            }
        }

        private void tryMoveDown(Graphics graphic, Engine engine)
        {
            if (positionQuadrantX < engine.GetMaxY() - 1)
            {
                int nextQuandrantX = this.positionQuadrantX;
                int nextQuadrantY = this.positionQuadrantY+1;
                string[] elements = engine.getQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[2] == "0")
                {
                    this.clearPacMan(graphic);

                    if (elements[4] == "1")
                    {
                        eatPoints += int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Down");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Down";
                    engine.updateMatrihElements(this.positionQuadrantX, this.positionQuadrantY, elements);

                }
                else if (elements[3] == "2" && positionQuadrantX < engine.GetMaxY() - 1)
                {
                    movedDirection = previousDirection;
                    this.move(graphic, engine, movedDirection);
                }
            }
        }

        private void clearPacMan(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color.Black), ((positionQuadrantX*50)+25 - (diameter / 2) - 1),
                   ((positionQuadrantY*50)+25 - (diameter / 2) - 1), diameter + 2, diameter + 2);
        }

        public void drawPacMan(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(pacManColor), (positionQuadrantX*50)+25 - (diameter / 2),
                ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);
        }

        async public void movePacMan(Graphics graphics, int nextX, int nextY, string moving)
        {
            switch (moving)
            {
                case "Right":
                    for (int x = (positionQuadrantX * 50) + 25; x < (nextX*50)+25; x++)
                    {                
                        graphics.DrawEllipse(new Pen(Color.Black),new Rectangle(x - 1 - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter) );

                        graphics.FillEllipse(new SolidBrush(pacManColor), x - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        System.Threading.Thread.Sleep(30);
                    }
                    break;

                case "Left":
                    for (int x = (positionQuadrantX * 50) + 25; x > (nextX * 50) + 25; x--)
                    {
                        graphics.DrawEllipse(new Pen(Color.Black),new Rectangle(x + 1 - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter) );

                        graphics.FillEllipse(new SolidBrush(pacManColor), x - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        System.Threading.Thread.Sleep(30);
                    }
                    break;

                case "Up":
                    for (int y = (this.positionQuadrantY * 50) + 25; y > (nextY * 50) + 25; y--)
                    {
                        graphics.DrawEllipse(new Pen(Color.Black),new Rectangle((positionQuadrantX * 50) + 25 - (diameter / 2),
                            y + 1 - (diameter / 2), diameter, diameter) );

                        graphics.FillEllipse(new SolidBrush(pacManColor), (positionQuadrantX*50)+25 - (diameter / 2),
                            y - (diameter / 2), diameter, diameter);

                        System.Threading.Thread.Sleep(30);
                    }
                    break;

                case "Down":
                    for (int y = (this.positionQuadrantY * 50) + 25; y < (nextY * 50) + 25; y++)
                    {
                        graphics.DrawEllipse(new Pen(Color.Black),new Rectangle((positionQuadrantX * 50) + 25 - (diameter / 2),
                            y - 1 - (diameter / 2), diameter, diameter) );

                        graphics.FillEllipse(new SolidBrush(pacManColor), (positionQuadrantX*50)+25 - (diameter / 2),
                            y - (diameter / 2), diameter, diameter);

                        System.Threading.Thread.Sleep(30);
                    }
                    break;
            }
        }

        private void initializePacMan(Graphics graphics,Engine engine)
        {
            string[] elements = engine.getQuadrantElements(this.positionQuadrantX, this.positionQuadrantY);

          
                if (elements[4] == "1")
                {
                    eatPoints += int.Parse(elements[4]);
                    elements[4] = "0";

                }
                this.drawPacMan(graphics);
                engine.updateMatrihElements(this.positionQuadrantX, this.positionQuadrantY, elements);

        }

    }
}
