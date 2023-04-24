using System.Drawing;

namespace corel_draw.Figures
{
    internal  class Square : Figure
    {
        public Square() { }
        public Square(int x, int y, int width, int height) : base(new Point(x, y), width,height)
        {
        }
        public override void Draw(Graphics g)
        { 
            g.DrawRectangle(Pen, Location.X, Location.Y, Width, Width);
        }
        public override void Fill(Graphics g)
        {
            g.FillRectangle(Brush, Location.X, Location.Y, Width, Width);
        }
        public override double CalcArea()
        {
            return Width * Width;
        }
        public override Figure Clone()
        {
            Square  clone = (Square)base.Clone();
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
