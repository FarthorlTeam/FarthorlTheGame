using System;
using System.Threading;

namespace FarthorlPacMan
{
    using System.Drawing;

    class Engine
    {
        private Graphics graphics;
        private Thread threadRendering;
        public Engine(Graphics graphic)
        {
            this.graphics = graphic;
        }

        public void initialize()
        {
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
    }
}
