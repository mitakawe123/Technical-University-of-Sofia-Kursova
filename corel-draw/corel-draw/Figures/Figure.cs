using System.Collections.Generic;
using System.Drawing;

namespace corel_draw.Figures
{
    internal abstract class Figure
    {
        private Point _location;
        private int _width;
        private int _height;
        public Color Color { get; set; }
        public string Name { get; set; }

        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public int Width 
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public Figure(int x, int y, int width, int height)
        {
            _location = new Point(x, y);
            _width = width;
            _height = height;
            Color = Color.Black; 
        }

        public abstract void Draw(Graphics g);
        public abstract double CalcArea();
        public virtual bool Contains(Point point) => _location.X <= point.X && point.X <= _location.X + _width && _location.Y <= point.Y && point.Y <= _location.Y + _height;
        
    }
}
