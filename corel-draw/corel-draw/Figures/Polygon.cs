using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal  class Polygon : Figure
    {
        private int _sides;
        public int Sides { get; set; }
        public Polygon(int x,int y,int width,int height, int sides):base(x,y,width,height)
        {
            this._sides = sides;
        }

        public Polygon() : base(0,0,0,0){}

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[]
            {
            new Point(100, 100),
            new Point(150, 150),
            new Point(200, 100),
            new Point(150, 50)
            };
            g.DrawPolygon(Pens.Black, points);
        }
        public override void CalcArea()
        {
            
        }
    }
}
