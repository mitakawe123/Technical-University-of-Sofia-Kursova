using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal  class Square : Figure
    {
        public Square(int x, int y, int width, int height) : base(x, y, width,height)
        {
        }
        public override void Draw(Graphics g)
        { 
            g.DrawRectangle(new Pen(Color, 5), Location.X, Location.Y, Width, Width);
        }
        public override double CalcArea()
        {
            return Width * Width;
        }
    }
}
