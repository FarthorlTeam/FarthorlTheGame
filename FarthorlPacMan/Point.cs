using System;

namespace FarthorlPacMan
{
    using System.Drawing;
    class Point
    {
        private int centerX;
        private int centerY;
        private Color pointColor=Color.Blue;
        private Color pointFillColor=Color.BlueViolet;
        private const int pointDiameter = 10;
        private Boolean isCollected = false;
        public Point(int centerX, int centerY)
        {
            this.centerX = centerX;
            this.centerY = centerY;
        }

        public void drawPoint(Graphics graphics)
        {
            if (isCollected)//Remove the point from the screen if the point is collected
            {
                graphics.FillEllipse(new SolidBrush(Color.Black), (centerX - (pointDiameter/2) - 1),
                    (centerY - (pointDiameter/2) - 1), pointDiameter + 2, pointDiameter + 2);
            }
            else // Draw the point if is not collected
            {
                graphics.DrawEllipse(new Pen(pointColor), (centerX - pointDiameter / 2), (centerY - pointDiameter / 2),
                    pointDiameter, pointDiameter);
                graphics.FillEllipse(new SolidBrush(pointFillColor), (centerX - (pointDiameter / 2) + 1),
                    (centerY - (pointDiameter / 2) + 1), pointDiameter - 1, pointDiameter - 1);
            }
        }

        public void eatPoint()
        {
            this.isCollected = true;
        }

        public int getX()
        {
            return this.centerX;
        }

        public int getY()
        {
            return this.centerY;
        }

    }
}
