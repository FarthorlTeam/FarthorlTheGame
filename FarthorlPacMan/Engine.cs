using System;
using System.IO;
using System.Threading;

namespace FarthorlPacMan
{
    using System.Drawing;

    class Engine
    {
        private Graphics graphics;
        private Thread threadRendering;
        private string[,] matrix;
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

        private void render()
        {

            while (true)
            {
                graphics.FillRectangle(new SolidBrush(Color.Black), 0,0 , 1200,800 );
            }
        }

        private void initializeMatrix()
        {
            try
            {
                using (var fileMatrix = new StreamReader("DataFile/coordinates.txt"))
                {
                    string inputLine;
                    while ((inputLine=fileMatrix.ReadLine())!=null)
                    {
                        var splitLine = inputLine.Trim().Split('='); // Get valies from the coordinates.txt example splitLine[0]=1,0 splitLine[1]=1|0|0|1|1
                        var arrayXYValues = splitLine[0].Trim().Split(','); //Get value of 2D array example arrayXYValues[0]=1 arrayXYValues[0]=0
                        int arrayX;
                        int arrayY;
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

                        this.matrix[arrayX, arrayY] = arrayValue;

                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }


        }
    }
}
