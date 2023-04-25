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

        public override void BeginCreateFigure()
        {
            _isDrawing = false;
            _isScrolling = false;
        }

        public override void MouseDown(MouseEventArgs e)
        {
            _startPoint = e.Location;
            _isDrawing = true;
        }

        public override void MouseMove(MouseEventArgs e)
        {
            if (_isDrawing && !_isScrolling)
            {
                _endPoint = e.Location;

                int x = (_startPoint.X + _endPoint.X) / 2;
                int y = (_startPoint.Y + _endPoint.Y) / 2;
                int radius = (int)Math.Sqrt(Math.Pow(_startPoint.X - x, 2) + Math.Pow(_startPoint.Y - y, 2));
                _circle = new Circle(x - radius, y - radius, radius);
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            _isDrawing = false;
            OnFinished(_circle);
        }

        public override void MouseWheel(MouseEventArgs e, Figure currentFigure)
        {
            _isScrolling = true;

            if (e.Delta > 0)
            {
                currentFigure.Height += SCALE_SUFFIX;
                currentFigure.Width += SCALE_SUFFIX;
            }
            else
            {
                currentFigure.Height -= SCALE_SUFFIX;
                currentFigure.Width -= SCALE_SUFFIX;
            }
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
