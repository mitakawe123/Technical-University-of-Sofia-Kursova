using corel_draw.Figures;
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
        private bool _isDrawing;
        public override void BeginCreateFigure()
        {            
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

                int x = Math.Min(_startPoint.X, _endPoint.X);
                int y = Math.Min(_startPoint.Y, _endPoint.Y);
                int width = Math.Abs(_startPoint.X - _endPoint.X);
                int height = Math.Abs(_startPoint.Y - _endPoint.Y);

                _rectangle = new Figures.Rectangle(x,y,width,height);
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            _isDrawing = false;
            OnFinished(_rectangle);
        }

        public override void Draw(Graphics g)
        {
            if(_rectangle != null)
            {
                g.DrawRectangle(_defaultPen, _rectangle.Location.X, _rectangle.Location.Y, _rectangle.Width, _rectangle.Height);
            }
        }
    }
}
