using System.Drawing;

namespace corel_draw.Figures
{
    internal  class Square : Figure
    {
        public Square(int x, int y, int width, int height) : base(new Point(x, y), width,height)
        {
        }
        
        public override void Draw(Graphics g) => g.DrawRectangle(Pen, Location.X, Location.Y, Width, Width);
        
        public override void Fill(Graphics g) => g.FillRectangle(Brush, Location.X, Location.Y, Width, Width);

        public override double CalcArea() => Width * Width;
        
        public override Figure Clone() => new Square(Location.X, Location.Y, Width, Width) { Color = Color, Name = Name, FillColor = FillColor };
    }
}
