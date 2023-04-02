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
        private int _sides;
        private List<Point> _points;

        public List<Point> Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public Polygon(List<Point> coordinates) : base(0, 0, 0, 0)
        {
            _points = coordinates;
            _sides = coordinates.Count;
        }

        public Polygon(List<Point> coordinates, int x, int y, int width, int height) : base(x, y, width, height)
        {
            _points = coordinates;
            _sides = coordinates.Count;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = _points.ToArray();
            g.DrawPolygon(new Pen(Color, 5), points);
        }

        public override bool Contains(Point point)
        {
            if (_points == null || _points.Count < 3)
                return false;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(_points.ToArray());
                return path.IsVisible(point);
            }
        }

        public override void CalcArea()
        {

        }
    }
}
