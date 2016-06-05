using System;
using System.Drawing;
using System.Windows.Forms;

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
                if (direction == "Right")
                {
                    tryMoveRight(graphic, engine);
                } else if (direction == "Left")
                {
                    tryMoveLeft(graphic,engine);
                } else if (direction == "Up")
                {
                    tryMoveUp(graphic, engine);
                } else if (direction == "Down")
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
                string[] elements = engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[3]=="0")
                {

                    if (elements[4]=="1")
                    {
                        eatPoints = eatPoints+int.Parse(elements[4]);
                        elements[4] = "0";
                        
                    }

                    this.movePacMan(graphic, nextQuandrantX,nextQuadrantY,"Right");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Right";
                    movedDirection = "Right";
                    engine.EatPointAdnUpdateMatrix(this.positionQuadrantX, this.positionQuadrantY, elements);

                } else if (elements[3]=="1" && positionQuadrantX < engine.GetMaxX()-1 )
                {
                    if (movedDirection=="")
                    {
                        previousDirection = "";
                    }
                    movedDirection = "";
                    this.move(graphic, engine,previousDirection);
                }
            }
        }

        private void tryMoveLeft(Graphics graphic, Engine engine)
        {

            if (positionQuadrantX > 0)
            {
                int nextQuandrantX = this.positionQuadrantX - 1;
                int nextQuadrantY = this.positionQuadrantY;
                string[] elements = engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[1] == "0")
                {
                    //this.clearPacMan(graphic);

                    if (elements[4] == "1")
                    {
                        eatPoints = eatPoints + int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Left");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Left";
                    movedDirection = "Left";
                    engine.EatPointAdnUpdateMatrix(this.positionQuadrantX, this.positionQuadrantY, elements);

                }
                else if (elements[1] == "1" && positionQuadrantX > 0 )
                {
                    if (movedDirection == "")
                    {
                        previousDirection = "";
                    }
                    movedDirection = "";
                    this.move(graphic, engine, previousDirection);
                }
            }
        }

        private void tryMoveUp(Graphics graphic, Engine engine)
        {
            if (positionQuadrantY > 0)
            {
                int nextQuandrantX = this.positionQuadrantX;
                int nextQuadrantY = this.positionQuadrantY-1;
                string[] elements = engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[2] == "0")
                {
                    //this.clearPacMan(graphic);

                    if (elements[4] == "1")
                    {
                        eatPoints = eatPoints + int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Up");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Up";
                    movedDirection = "Up";
                    engine.EatPointAdnUpdateMatrix(this.positionQuadrantX, this.positionQuadrantY, elements);

                }
                else if (elements[1] == "1" && positionQuadrantX > 0 )
                {
                    if (movedDirection == "")
                    {
                        previousDirection = "";
                    }
                    movedDirection = "";
                    this.move(graphic, engine, previousDirection);
                }
            }
        }

        private void tryMoveDown(Graphics graphic, Engine engine)
        {
            if (positionQuadrantY < engine.GetMaxY() - 1)
            {
                int nextQuandrantX = this.positionQuadrantX;
                int nextQuadrantY = this.positionQuadrantY+1;
                string[] elements = engine.GetQuadrantElements(nextQuandrantX, nextQuadrantY);

                if (isAlive && elements[0] == "0")
                {
                    //this.clearPacMan(graphic);

                    if (elements[4] == "1")
                    {
                        eatPoints = eatPoints + int.Parse(elements[4]);
                        elements[4] = "0";
                    }

                    this.movePacMan(graphic, nextQuandrantX, nextQuadrantY, "Down");
                    this.positionQuadrantX = nextQuandrantX;
                    this.positionQuadrantY = nextQuadrantY;
                    previousDirection = "Down";
                    movedDirection = "Down";
                    engine.EatPointAdnUpdateMatrix(this.positionQuadrantX, this.positionQuadrantY, elements);

                }
                else if (elements[0] == "1" && positionQuadrantY < engine.GetMaxY())
                {
                    if (movedDirection == "")
                    {
                        previousDirection = "";
                    }
                    movedDirection ="";
                    this.move(graphic, engine, previousDirection);
                }
            }
        }

        public void drawPacMan(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(pacManColor), (positionQuadrantX*50)+25 - (diameter / 2),
                ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);
        }

        async public void movePacMan(Graphics graphics, int nextX, int nextY, string moving)
        {
            graphics.FillRectangle(new SolidBrush(Color.Black), (positionQuadrantX * 50) + 25 - (diameter / 2) - 3,
                ((positionQuadrantY * 50) + 25 - (diameter / 2) - 3), diameter + 6, diameter + 6);

            switch (moving)
            {
                case "Right":
                    for (int x = (positionQuadrantX * 50) + 25; x < (nextX*50)+25; x++)
                    {                
                        graphics.DrawEllipse(new Pen(Color.Black),new Rectangle(x-1 - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter) );

                        graphics.FillEllipse(new SolidBrush(pacManColor), x - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        System.Threading.Thread.Sleep(10);
                    }
                    break;

                case "Left":
                    for (int x = (positionQuadrantX * 50) + 25; x > (nextX * 50) + 25; x--)
                    {
                        graphics.DrawEllipse(new Pen(Color.Black),new Rectangle(x+1  - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter) );

                        graphics.FillEllipse(new SolidBrush(pacManColor), x - (diameter / 2),
                            ((positionQuadrantY * 50) + 25 - (diameter / 2)), diameter, diameter);

                        System.Threading.Thread.Sleep(10);
                    }
                    break;

                case "Up":
                    for (int y = (this.positionQuadrantY * 50) + 25; y > (nextY * 50) + 25; y--)
                    {
                        graphics.DrawEllipse(new Pen(Color.Black),new Rectangle((positionQuadrantX * 50) + 25 - (diameter / 2),
                            y+1 - (diameter / 2), diameter, diameter) );

                        graphics.FillEllipse(new SolidBrush(pacManColor), (positionQuadrantX * 50)+25 - (diameter / 2),
                            y - (diameter / 2), diameter, diameter);

                        System.Threading.Thread.Sleep(10);
                    }
                    break;

                case "Down":
                    for (int y = (this.positionQuadrantY * 50) + 25; y < (nextY * 50) + 25; y++)
                    {
                        graphics.DrawEllipse(new Pen(Color.Black),new Rectangle((positionQuadrantX * 50) + 25 - (diameter / 2),
                            y-1 - (diameter / 2), diameter, diameter) );

                        graphics.FillEllipse(new SolidBrush(pacManColor), (positionQuadrantX*50)+25 - (diameter / 2),
                            y - (diameter / 2), diameter, diameter);

                        System.Threading.Thread.Sleep(10);
                    }
                    break;
            }
        }

        private void initializePacMan(Graphics graphics,Engine engine)
        {
            string[] elements = engine.GetQuadrantElements(this.positionQuadrantX, this.positionQuadrantY);

          
                if (elements[4] == "1")
                {
                    eatPoints = eatPoints+int.Parse(elements[4]);
                    elements[4] = "0";
                }
                this.drawPacMan(graphics);
                engine.EatPointAdnUpdateMatrix(this.positionQuadrantX, this.positionQuadrantY, elements);
        }

        public int getScore()
        {
            return this.eatPoints;
        }

    }
}
