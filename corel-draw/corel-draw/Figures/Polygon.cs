using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal class Polygon : Figure
    {
        private readonly List<Point> _points;

        public Polygon(List<Point> coordinates) : base(0, 0, 0, 0)
        {
            _points = coordinates;
        }

        public void Move(Point newPoint)
        {
            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = new Point(_points[i].X + newPoint.X, _points[i].Y + newPoint.Y);
            }
        }

        public override void Draw(Graphics g)
        {
            Point[] points = _points.ToArray();
            g.DrawPolygon(new Pen(Color, 5), points);
        }

        public void editPolygon(List<Point> newCoordinates)
        {
            _points.Clear();
            _points.AddRange(newCoordinates);
        }

        //Jordan Curve Theorem
        public override bool Contains(Point point)
        {
            bool inside = false;
            int count = _points.Count;

            for (int i = 0, j = count - 1; i < count; j = i++)
            {
                if (((_points[i].Y > point.Y) != (_points[j].Y > point.Y)) &&
                    (point.X < (_points[j].X - _points[i].X) * (point.Y - _points[i].Y) / (_points[j].Y - _points[i].Y) + _points[i].X))
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        public override double CalcArea()
        {
            double area = 0;

            //Shoelace formula
            int j = _points.Count - 1;
            for (int i = 0; i < _points.Count; i++)
            {
                area += (_points[j].X + _points[i].X) * (_points[j].Y - _points[i].Y);
                j = i;
            }

            return Math.Abs(area / 2);
        }
    }
}
