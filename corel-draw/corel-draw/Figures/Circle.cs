using System;
using System.Drawing;

namespace corel_draw.Figures
{
    internal class Circle:Figure
    {
        public Circle() { }
        public Circle(int x, int y, int radius) : base(new Point(x, y), radius * 2, radius * 2)
        {
        }
        public override double CalcArea()
        {
            double radius = Width / 2.0;
            return Math.PI * radius * radius;
        }
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(Pen, Location.X, Location.Y, Width, Height);
        }
        public override void Fill(Graphics g)
        {
            g.FillEllipse(Brush, Location.X, Location.Y, Width, Height);
        }
        public override Figure Clone()
        {
            Circle clone = (Circle)base.Clone();
            clone.Location = Location;
            clone.Width = Width;
            clone.Height = Height;
            clone.Name = Name;
            clone.Color = Color;
            clone.FillColor = FillColor;
            return clone;
        }
    }
}
