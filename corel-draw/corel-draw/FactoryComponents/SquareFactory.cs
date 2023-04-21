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
            _square = new Square();
        }
        public override void MouseDown(MouseEventArgs e)
        {
            _startPoint = e.Location;
            _endPoint = e.Location;
        }

        public override void MouseMove(MouseEventArgs e)
        {
            _endPoint = e.Location;
        }

        public override void MouseUp(MouseEventArgs e)
        {
            int x = Math.Min(_startPoint.X, _endPoint.X);
            int y = Math.Min(_startPoint.Y, _endPoint.Y);
            int width = Math.Abs(_endPoint.X - _startPoint.X);
            int height = width;
            _square.Location = new Point(x, y);
            _square.Width = width;
            _square.Height = height;
            Finished?.Invoke(_square);
        }
    }
}
