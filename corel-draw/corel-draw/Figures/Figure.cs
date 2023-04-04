using System.Collections.Generic;
using System.Drawing;

namespace corel_draw.Figures
{
    internal abstract class Figure
    {
        protected Point location;
        protected int width;
        protected int height;
        public Color Color { get; set; }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }
        public int Width 
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }


        public Figure(int x, int y, int width, int height)
        {
            location = new Point(x, y);
            this.width = width;
            this.height = height;
            Color = Color.Black; 
        }

        public abstract void Draw(Graphics g);
        public abstract void CalcArea();
        public virtual bool Contains(Point point)
        {
            return location.X <= point.X && point.X <= location.X + width && location.Y <= point.Y && point.Y <= location.Y + height;
        }
    }
}
