using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarthorlPacMan
{
    class Point
    {
        public int centerX;
        public int centerY;
        private Color pointColor=Color.Blue;
        private const int pointRadius = 10;
        public Point(int centerX, int centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
        }

        public void drawPoint(Graphics graphics)
        {
            graphics.DrawEllipse(new Pen(pointColor), ((centerX * 50) + 25) - pointRadius, ((centerY * 50) + 25) - pointRadius, ((centerX * 50) + 25) + pointRadius, ((centerY * 50) + 25) + pointRadius);
        }

    }
}
