using System.Drawing;

namespace corel_draw.Figures
{
    internal class Rectangle : Figure
    {
        public Rectangle(int x, int y, int width, int height) : base(new Point(x, y), width, height) {}

        public override double CalcArea() => Width * Height;

        public override void Draw(Graphics g) => g.DrawRectangle(Pen, Location.X, Location.Y, Width, Height);

        public override void Fill(Graphics g) => g.FillRectangle(Brush, Location.X, Location.Y, Width, Height);

        public override Figure Clone() => new Rectangle(Location.X, Location.Y, Width, Height) { Color = Color, Name = Name, FillColor = FillColor };
    }
}
