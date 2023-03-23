using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal class Rectangle : Figure
    {
        public override void CalcArea()
        {
        }

        public Rectangle(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        public Rectangle() : base(0, 0, 0, 0) { }

        public override void Draw(Graphics g)
        {            
            g.DrawRectangle(Pens.Black, location.X, location.Y, width, height);
        }
    }
}
