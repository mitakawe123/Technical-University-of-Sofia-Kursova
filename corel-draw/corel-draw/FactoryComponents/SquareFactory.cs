using corel_draw.Figures;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace corel_draw.FactoryComponents
{
    internal class SquareFactory : FigureFactory
    {
        private Point _startPoint;
        private Point _endPoint;
        private Square _square;

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
            if(_isDrawing)
            {
                _endPoint = e.Location;

                int x = Math.Min(_startPoint.X, _endPoint.X);
                int y = Math.Min(_startPoint.Y, _endPoint.Y);
                int width = Math.Abs(_endPoint.X - _startPoint.X);
                int height = width;

                _square = new Square(x,y,width,height);
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            _isDrawing = false;
            OnFinished(_square);
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
            if (_square != null)
            {
                g.DrawRectangle(_defaultPen, _square.Location.X, _square.Location.Y, _square.Width, _square.Height);
            }
        }
    }
}
