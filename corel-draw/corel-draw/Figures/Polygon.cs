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

        public Polygon(int x, int y, int width, int height, PolygonSides polygonSides) : base(x, y, width, height)
        {
            this.polygonSides = polygonSides;
            _sides = polygonSides.Sides;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[_sides];
            g.DrawPolygon(Pens.Black, points);
        }

        public override void CalcArea()
        {

        }
    }
}
