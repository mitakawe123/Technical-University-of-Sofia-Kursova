using System;
using System.Drawing;

namespace corel_draw.Figures
{
    internal class Circle:Figure
    {
        public Circle(int x, int y, int radius) : base(new Point(x, y), radius * 2, radius * 2)
        {
            Color = Color.Black;

        }
        public override double CalcArea()
        {
            double radius = Width / 2.0;
            return Math.PI * radius * radius;
        }
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(_pen, Location.X, Location.Y, Width, Height);
        }
        public override void Fill(Graphics g)
        {
            g.FillEllipse(_brush, Location.X, Location.Y, Width, Height);
        }
        public override Figure Clone()
        {
            return new Circle(Location.X, Location.Y, Width / 2) { Color = Color, Name = Name, FillColor = FillColor };
        }
    }
}
