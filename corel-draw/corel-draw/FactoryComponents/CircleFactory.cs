using corel_draw.Figures;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace corel_draw.FactoryComponents
{
    internal class CircleFactory : FigureFactory
    {
        private Point _startPoint;
        private Point _endPoint;
        private Circle _circle;
        private bool _isDrawing;
        public override void BeginCreateFigure()
        {
            _circle = new Circle();
            _isDrawing = false;
        }

        public override void MouseDown(MouseEventArgs e)
        {
            _startPoint = e.Location;
            _isDrawing = true;
        }

        public override void MouseMove(MouseEventArgs e)
        {
            if (_isDrawing)
            {
                _endPoint = e.Location;

                int x = (_startPoint.X + _endPoint.X) / 2;
                int y = (_startPoint.Y + _endPoint.Y) / 2;
                int radius = (int)Math.Sqrt(Math.Pow(_startPoint.X - x, 2) + Math.Pow(_startPoint.Y - y, 2));

                _circle.Location = new Point(x - radius, y - radius);
                _circle.Width = radius * 2;
                _circle.Height = radius * 2;
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            _isDrawing = false;
            OnFinished(_circle);
        }

        public override void Draw(Graphics g)
        {
            if (_circle != null)
            {
                g.DrawEllipse(_defaultPen, _circle.Location.X, _circle.Location.Y, _circle.Width, _circle.Height);
            }
        }
    }
}
