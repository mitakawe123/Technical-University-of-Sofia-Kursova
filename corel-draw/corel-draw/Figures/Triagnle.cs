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
        public Triagnle(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[]
            {
                new Point(100, 100),
                new Point(150, 150),
                new Point(150, 150)
            };
            g.DrawPolygon(Pens.Black, points);   
        }

        public override void CalcArea()
        {
        }
    }
}
