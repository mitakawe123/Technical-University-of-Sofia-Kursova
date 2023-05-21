using System;
using System.Drawing;

namespace corel_draw.Figures
{
    internal class Circle: Figure
    {
        public Circle(int x, int y, int radius) : base(new Point(x, y), radius * 2, radius * 2) {}

        public override double CalcArea() => Math.PI * (Width / 2.0) * (Width / 2.0);

        public override void Draw(Graphics g) => g.DrawEllipse(Pen, Location.X, Location.Y, Width, Height);

        public override void Fill(Graphics g) => g.FillEllipse(Brush, Location.X, Location.Y, Width, Height);

        public override Figure Clone() => new Circle(Location.X, Location.Y, Width / 2) { Color = Color, Name = Name, FillColor = FillColor };
    }
}
