using System.Drawing;

namespace corel_draw.Figures
{
    internal class Rectangle : Figure
    {
        public Rectangle(int x, int y, int width, int height) : base(new Point(x, y), width, height)
        {
        }
        public override double CalcArea() {
            return Width * Height;
        }
        public override void Draw(Graphics g)
        {            
            g.DrawRectangle(new Pen(Color, 5), Location.X, Location.Y, Width, Height);
        }
        public override Figure Clone()
        {
            return new Rectangle(Location.X, Location.Y, Width, Height) { Color = Color, Name = Name };
        }
    }
}
