using System;
using System.Drawing;

namespace corel_draw.Figures
{
    internal class Circle:Figure
    { 
        public Circle(int x,int y,int width, int height) : base(new Point(x,y), width, height)
        {
        }
        public override double CalcArea()
        {
            double radius = Width / 2.0;
            return Math.PI * radius * radius;
        }
        public override void Draw(Graphics g)
        { 
           g.DrawEllipse(new Pen(Color, 5), Location.X, Location.Y, Width, Height);
        }
        public override void Fill(Graphics g)
        {
            g.FillEllipse(new SolidBrush(FillColor),Location.X,Location.Y,Width,Height); 
        }
        public override Figure Clone()
        {
            return new Circle(Location.X, Location.Y, Width, Height) { Color = Color, Name = Name, FillColor = FillColor };
        }
    }
}
