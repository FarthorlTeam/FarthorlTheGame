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
        private Color pointFillColor=Color.BlueViolet;
        private const int pointDiameter = 10;
        public Point(int centerX, int centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
        }

        public void drawPoint(Graphics graphics)
        {
            graphics.DrawEllipse(new Pen(pointColor), (centerX) , (centerY) , pointDiameter, pointDiameter);
            graphics.FillEllipse(new SolidBrush(pointFillColor), (centerX+1), (centerY+1), pointDiameter - 1, pointDiameter - 1);
        }

    }
}
