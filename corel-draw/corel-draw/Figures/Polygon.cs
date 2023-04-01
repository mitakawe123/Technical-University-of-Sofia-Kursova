using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal class Polygon : Figure
    {
        private int _sides;
        private List<Point> _points;

        public Polygon(List<Point> coordinates, int x, int y, int width, int height) : base(x, y, width, height)
        {
            _points = coordinates;
            _sides = coordinates.Count;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = _points.ToArray();
            g.DrawPolygon(Pens.Black, points);
        }

        public override void CalcArea()
        {

        }
    }
}
