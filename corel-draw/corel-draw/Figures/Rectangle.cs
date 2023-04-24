using System.Drawing;

namespace corel_draw.Figures
{
    internal class Rectangle : Figure
    {
        public Rectangle() { }
        public Rectangle(int x, int y, int width, int height) : base(new Point(x, y), width, height)
        {
        }
        public override double CalcArea() {
            return Width * Height;
        }
        public override void Draw(Graphics g)
        {            
            g.DrawRectangle(Pen, Location.X, Location.Y, Width, Height);
        }
        public override void Fill(Graphics g)
        {
            g.FillRectangle(Brush, Location.X, Location.Y, Width, Height);
        }
        public override Figure Clone()
        {
            Rectangle clone = (Rectangle)base.Clone();
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
