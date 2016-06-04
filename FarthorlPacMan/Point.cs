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
        private int centerX;
        private int centerY;
        private Color pointColor=Color.Blue;
        private Color pointFillColor=Color.BlueViolet;
        private const int pointDiameter = 10;
        private int pointStatus = 1;
        public Point(int centerX, int centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
        }

        public void drawPoint(Graphics graphics)
        {
            if (pointStatus == 1) //Draw the point if is not collected
            {
                graphics.DrawEllipse(new Pen(pointColor), (centerX - pointDiameter/2), (centerY - pointDiameter/2),
                    pointDiameter, pointDiameter);
                graphics.FillEllipse(new SolidBrush(pointFillColor), (centerX - (pointDiameter/2) + 1),
                    (centerY - (pointDiameter/2) + 1), pointDiameter - 1, pointDiameter - 1);
            }
            else //Remove the point from the screen if the point is collected
            {
                graphics.FillEllipse(new SolidBrush(Color.Black), (centerX - (pointDiameter / 2)-1),
                    (centerY - (pointDiameter / 2)-1), pointDiameter+2, pointDiameter+2);
            }
        }

        public void eatPoint()
        {
            this.pointStatus = 0;
        }

    }
}
