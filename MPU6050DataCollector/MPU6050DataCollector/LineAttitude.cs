using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace MPU6050DataCollector
{
    class LineAttitude
    {
        private Line line;
        private double canvasWidth, canvasHeight;
        public LineAttitude(Canvas canv)
        {
            this.line = new Line();
            this.canvasWidth = canv.Width;
            this.canvasHeight = canv.Height;
        }

        public Line getLine(double x)
        {
            double angle = x - 90;
            double origin = this.canvasWidth / 2;
            this.line.Visibility = System.Windows.Visibility.Visible;
            this.line.Stroke = System.Windows.Media.Brushes.Black;

            double lineLength = this.canvasWidth / 4;

            this.line.X1 = origin - Math.Cos(angle * Math.PI / 180) * lineLength;
            this.line.Y1 = origin - Math.Sin(angle * Math.PI / 180) * lineLength;

            this.line.X2 = origin + Math.Cos(angle * Math.PI / 180) * lineLength;
            this.line.Y2 = origin + Math.Sin(angle * Math.PI / 180) * lineLength;


            this.line.StrokeThickness = 7;
            return line;
        }


    }
}
