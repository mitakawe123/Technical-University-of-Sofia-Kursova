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
        private List<Point> _points;

        public List<Point> Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public Polygon(List<Point> coordinates) : base(0, 0, 0, 0)
        {
            _points = coordinates;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = _points.ToArray();
            g.DrawPolygon(new Pen(Color, 5), points);
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

        public override void CalcArea()
        {
            // TODO: Calculate the area of the polygon
        }
    }
}
