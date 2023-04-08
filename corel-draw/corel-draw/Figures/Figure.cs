using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace corel_draw.Figures
{
    internal class Figure
    {
        private Point _location;
        private int _width;
        private int _height;
        private List<Point> _points;

        public Color Color { get; set; }
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<Point> Points 
        {
            get { return _points; } 
            set { _points = value; } 
        }
        public virtual Point Location
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
        public Figure() 
        {
            //need empty constructor for deserialize the data in the json file
        }

        public Figure(int x, int y, int width, int height)
        {
            _location = new Point(x, y);
            _width = width;
            _height = height;
            Color = Color.Black;
        }
        public Figure(List<Point> coordinates) 
        {
            _points = coordinates;
            Color = Color.Black;
        }


        public virtual void Move(Point delta)
        {
            _location = new Point(_location.X + delta.X, _location.Y + delta.Y);
        }
        public Figure Clone()
        {
            if (this is Polygon)
                return new Polygon(_points.ToList());
            else
                return new Figure(_location.X, _location.Y, _width, _height);
        }

        public virtual void Draw(Graphics g) { }
        public virtual double CalcArea() { return 0; }
        public virtual bool Contains(Point point) => _location.X <= point.X && point.X <= _location.X + _width && _location.Y <= point.Y && point.Y <= _location.Y + _height;
    }
}
