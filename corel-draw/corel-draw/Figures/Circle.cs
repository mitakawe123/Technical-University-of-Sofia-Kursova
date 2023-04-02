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
        

        public override void CalcArea()
        {
        }
        public Circle(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }
        public override void Draw(Graphics g)
        { 
           g.DrawEllipse(new Pen(Color, 5), location.X, location.Y, width, height);
        }
    }
}
