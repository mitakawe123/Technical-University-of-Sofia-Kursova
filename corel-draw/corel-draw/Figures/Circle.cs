using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal class Circle:Figure
    {
        public override void DrawFigure(PaintEventArgs e, float x, float y, float width, float height)
        {
            base.DrawFigure(e, x, y, width, height);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 5);
            g.DrawEllipse(pen, x, y, width, height);
        }

        public double CalculateCircleArea(double radius)
        {
            
            double area = Math.PI * radius * radius;
            return area;
        }
        public Circle(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }

        public Circle() : base(0, 0, 0, 0) { }
    }
}
