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
        /*public override void DrawPolygon(PaintEventArgs e, float x, float y, float width, float height, int sides)
        {
            base.DrawPolygon(e, x, y, width, height, sides);
            PointF[] points = new PointF[sides];
            double angle = Math.PI * 2 / sides;
            double radius = Math.Min(width, height) / 2;
            PointF center = new PointF(x + width / 2, y + height / 2);
            for (int i = 0; i < sides; i++)
            {
                double x1 = center.X + radius * Math.Cos(angle * i);
                double y1 = center.Y + radius * Math.Sin(angle * i);
                points[i] = new PointF((float)x1, (float)y1);
            }
            e.Graphics.DrawPolygon(Pens.Black, points);
        }*/

        public Polygon(float x, float y, float width, float height, int sides) : base(x, y, width, height, sides)
        {
        }
    }
}
