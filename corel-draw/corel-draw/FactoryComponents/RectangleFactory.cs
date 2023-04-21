using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.FactoryComponents
{
    internal class RectangleFactory : FigureFactory
    {
        private Point _startPoint;
        private Point _endPoint;
        private Figures.Rectangle _rectangle;
        public override void BeginCreateFigure()
        {
            _rectangle = new Figures.Rectangle();
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
            int width = Math.Abs(_startPoint.X - _endPoint.X);
            int height = Math.Abs(_startPoint.Y - _endPoint.Y);
            _rectangle.Location = new Point(x, y);
            _rectangle.Width = width;
            _rectangle.Height = height;
            Finished?.Invoke(_rectangle);
        }
    }
}
