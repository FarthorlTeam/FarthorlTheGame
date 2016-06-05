using System.Windows.Forms;

namespace FarthorlPacMan
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Drawing;

    public class Engine
    {
        private Graphics graphics;
        private Thread threadRendering;
        private string[,] pathsMatrix=new string[24,16];
        private int xMax = 24;
        private int yMax = 16;
        private int leftScore;
        private bool run = true;
        private string moveDirection;
        private Color wallColor=Color.Cyan;
        private readonly GameWindows game;
        List<Point> points=new List<Point>();
        public Engine(Graphics graphic, GameWindows game)
        {
            this.graphics = graphic;
            this.game = game;
        }

        public void Initialize()
        {
            threadRendering = new Thread(new ThreadStart(render));
            initializeMatrix();
            DrawFontColor();
            drawPaths();
            foreach (var point in points)
            {
                point.drawPoint(graphics);
            }
            inicializeLeftScores();
            threadRendering.Start();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void stopGame()
        {
            threadRendering.Abort();
            run = false;
        }

        public void pauseGame()
        {
            this.run = false;
        }

        //Heare is the logic for gaming
        private void render()
        {
            PacMan pacMan = new PacMan(0, 0, this.graphics, this);
            Label scoreLabel=new Label();
            scoreLabel.Width=250;
            scoreLabel.Height = 50;
            scoreLabel.Left = 3;
            scoreLabel.Top = 802;
            scoreLabel.BringToFront();
            while (run)
            {

                pacMan.move(this.graphics,this, moveDirection);

                game.UpdateScores(pacMan.getScore());
                UpdateLeftSores(pacMan.getScore());

            }
        }

        private void initializeMatrix()
        {
            try
            {
                using (var fileMatrix = new StreamReader("DataFiles/coordinates.txt"))
                {
                    string inputLine;
                    while ((inputLine=fileMatrix.ReadLine())!=null)
                    {
                        // Get values from the coordinates.txt example splitLine[0]=1,0 splitLine[1]=1|0|0|1|1
                        var splitLine = inputLine.Trim().Split('=');

                        //Get the position values for the 2D array example arrayXYValues[0]=1 arrayXYValues[0]=0 
                        var arrayXYValues = splitLine[0].Trim().Split(','); 
                        int arrayX;
                        int arrayY;

                        //This is the values of the array cell
                        string arrayValue = splitLine[1];
                        try
                        {
                            arrayX = int.Parse(arrayXYValues[0]);
                            arrayY = int.Parse(arrayXYValues[1]);
                        }
                        catch (Exception)
                        {
                            throw new ArgumentException("Cannot conver string to integer");

                        }

                        //Add element data in to the specific point in the 2D array
                        this.pathsMatrix[arrayX, arrayY] = arrayValue;
                    }
                }
            }
            catch (Exception)
            {   
                throw new FileLoadException();
            }


        }

        private void drawPaths()
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    var elements = pathsMatrix[x,y].Trim().Split('|');
                    int topIndex = int.Parse(elements[0]);
                    int rightIndex = int.Parse(elements[1]);
                    int bottomIndex = int.Parse(elements[2]);
                    int leftIndex = int.Parse(elements[3]);
                    int pointIndex = int.Parse(elements[4]);

                    if (topIndex==1)
                    {
                        graphics.DrawLine(new Pen(wallColor), (x * 50), (y * 50), (x * 50) + 50, (y * 50));
                    }

                    if (rightIndex==1)
                    {
                       graphics.DrawLine(new Pen(wallColor), (x * 50) + 50, (y * 50), (x * 50) + 50, (y * 50) + 50);
                    }

                    if (bottomIndex==1)
                    {
                        graphics.DrawLine(new Pen(wallColor), (x * 50) , (y * 50) + 50, (x * 50) + 50, (y * 50) + 50);
                    }

                    if (leftIndex==1)
                    {
                        graphics.DrawLine(new Pen(wallColor), (x*50) , (y*50), (x*50) , (y*50) + 50);
                    }

                    if (pointIndex == 1)
                    {
                        Point point = new Point((x*50) + 25, (y*50) + 25);
                        points.Add(point);
                    }
                    else
                    {
                        graphics.FillRectangle(new SolidBrush(wallColor), (x * 50), (y * 50), 50, 50);
                    }

                }
            }

        }

        private void DrawFontColor()
        {
            graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, 1200, 800);
        }

        public int GetMaxX()
        {
            return this.xMax;
        }

        public int GetMaxY()
        {
            return this.yMax;
        }

        public string[] GetQuadrantElements(int quadrantX, int quandrantY)
        {
            string[] elements = pathsMatrix[quadrantX, quandrantY].Trim().Split('|');
            return elements;
        }

        public void EatPointAdnUpdateMatrix(int quadrantX, int quandrantY, string[] element)
        {
            var stringValue =$"{element[0]}|{element[1]}|{element[2]}|{element[3]}|{element[4]}";
            pathsMatrix[quadrantX, quandrantY] = stringValue;

            foreach (var point in points)
            {
                if (point.getX()==(quadrantX*50)+25 && point.getY()==(quandrantY*50)+25)
                {
                    point.eatPoint();
                    break;
                }
            }
        }

        public void changeDirection(string newDirection)
        {
            this.moveDirection = newDirection;
        }

        private void inicializeLeftScores()
        {
            foreach (var point in points)
            {
                if (!point.isEatPoint())
                {
                    leftScore = leftScore + 1;
                }
            }
            game.updateLeftScore(leftScore);
        }

        private void UpdateLeftSores(int pacMandScores)
        {
            game.updateLeftScore(leftScore - pacMandScores);
            if (leftScore-pacMandScores==0)
            {
                pauseGame();
                game.Win();
            }
        }

    }
}
