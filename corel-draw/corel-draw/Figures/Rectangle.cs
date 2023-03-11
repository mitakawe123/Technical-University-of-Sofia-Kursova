using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal  class Rectangle : Figure
    {
        private Rectangle rectangle;
        public override void DrawFigure(PaintEventArgs e, float x, float y, float width, float height)
        {
            base.DrawFigure(e, x, y, width, height);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 5);
            g.DrawRectangle(pen, x, y, width, height);
        }
      
        public Rectangle(float x, float y, float width, float height) : base(x, y, width, height)
        {
        }

        public Rectangle() : base(0, 0, 0, 0) { }
    }
}
