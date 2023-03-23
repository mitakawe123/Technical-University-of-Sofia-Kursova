using System.Drawing;

namespace corel_draw.Figures
{
    internal abstract class Figure
    {
        protected Point location;
        protected int width;
        protected int height;

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }
        public int Width { get; private set; }
        public int Height { get; private set; }


        public Figure(int x, int y, int width, int height)
        {
            location = new Point(x, y);
            this.width = width;
            this.height = height;
        }

        public abstract void Draw(Graphics g);
        public abstract void CalcArea();
        public bool Contains(Point point)
        {
            return location.X <= point.X && point.X <= location.X + width && location.Y <= point.Y && point.Y <= location.Y + height;
        }

        public void Move(int dx, int dy)
        {
            location = new Point(location.X + dx, location.Y + dy); 
        }
    }
}
