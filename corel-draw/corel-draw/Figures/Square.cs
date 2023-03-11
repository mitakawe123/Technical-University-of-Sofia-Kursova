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
        public override void DrawFigure(PaintEventArgs e, float x, float y, float width, float height)
        {
            base.DrawFigure(e, x, y, width, height);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 5);
            //setting height the same for square so that i don't make another function for draw square 
            g.DrawRectangle(pen, x, y, height, height);
        }
        public Square(float x, float y, float width,float height) : base(x, y, width,height)
        {
        }

        public Square():base(0,0,0,0) { }
    }
}
