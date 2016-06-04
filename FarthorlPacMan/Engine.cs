using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Schema;

namespace FarthorlPacMan
{
    using System.Drawing;

    class Engine
    {
        private Graphics graphics;
        private Thread threadRendering;
        private string[,] matrix=new string[24,16];
        private const int xMax = 24;
        private const int yMax = 16;
        private const int pointRadius = 10;
        private Color wallColor=Color.Cyan;
        List<Point> points=new List<Point>(); 
        public Engine(Graphics graphic)
        {
            this.graphics = graphic;
        }

        public void initialize()
        {
            initializeMatrix();
            threadRendering=new Thread(new ThreadStart(render));
            threadRendering.Start();
        }

        public void stopGame()
        {
            threadRendering.Abort();
        }

        //Heare is the logic for gaming
        private void render()
        {
            drawFont();
            drawPaths();

            while (true)
            {
                Random random=new Random();
                int number = random.Next(points.Count);
                points[number].eatPoint();

                //Redraw the points
                foreach (var point in points)
                {
                    point.drawPoint(graphics);
                }

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
                        this.matrix[arrayX, arrayY] = arrayValue;
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
                    var elements = matrix[x,y].Trim().Split('|');
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

        private void drawFont()
        {
            graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, 1200, 800);
        }
    }
}
