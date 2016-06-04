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
            threadRendering=new Thread(ThreadStart(render));
            threadRendering.Start();
        }
    }
}
