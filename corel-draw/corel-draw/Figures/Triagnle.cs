using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal  class Triagnle : Figure
    {
        public void DrawTriangle(Graphics g, Brush brush, PointF point1, PointF point2, PointF point3)
        {
            PointF[] points = { point1, point2, point3 };
            g.FillPolygon(brush, points);
        }
        public double CalculateTriangleArea(double baseLength, double height)
        {
            double area = 0.5 * baseLength * height;
            return area;
        }
        public Triagnle(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }
        public Triagnle() : base(0, 0, 0, 0) { }
    }
}
