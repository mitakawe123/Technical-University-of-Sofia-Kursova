using corel_draw.Figures;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace corel_draw.FactoryComponents
{
    internal class CircleFactory : FigureFactory
    {
        private Point startPoint;
        private Point endPoint;
        private Circle _circle;

        public override void BeginCreateFigure()
        {
            _circle = new Circle();
        }

        public override void MouseDown(MouseEventArgs e)
        {
            startPoint = e.Location;
        }
        [DebuggerStepThrough]
        public override void MouseMove(MouseEventArgs e)
        {
            endPoint = e.Location;
        }

        public override void MouseUp(MouseEventArgs e)
        {
            int x = (startPoint.X + endPoint.X) / 2;
            int y = (startPoint.Y + endPoint.Y) / 2;
            int radius = (int)Math.Sqrt(Math.Pow(startPoint.X - x, 2) + Math.Pow(startPoint.Y - y, 2));
            _circle.Location = new Point(x - radius, y - radius);
            _circle.Width = radius * 2;
            _circle.Height = radius * 2;
            OnFinished(_circle);
        }
    }
}
