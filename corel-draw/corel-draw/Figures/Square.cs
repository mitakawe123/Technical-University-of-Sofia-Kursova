using System.Drawing;

namespace corel_draw.Figures
{
    internal  class Square : Figure
    {
        public Square(int x, int y, int width, int height) : base(new Point(x, y), width,height)
        {
        }
        public override void Draw(Graphics g)
        { 
            g.DrawRectangle(new Pen(Color, 5), Location.X, Location.Y, Width, Width);
        }
        public override double CalcArea()
        {
            return Width * Width;
        }
    }
}
