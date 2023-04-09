using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

namespace corel_draw.Figures
{
    internal class Figure
    {
        private Point _location;
        private int _width;
        private int _height;
        private List<Point> _points;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Color Color { get; set; }
        public string Name { get; set; }
        public List<Point> Points 
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

        public virtual void Move(Point delta)
        {
            _location = new Point(_location.X + delta.X, _location.Y + delta.Y);
        }
        public Figure Clone()
        {
            if (this is Polygon)
                return new Polygon(_points);
            else
                return new Figure(_location.X, _location.Y, _width, _height);
        }
        public void CopyState(Figure figure)
        {
            if (figure is Polygon && this is Polygon)
            {
                Points = new List<Point>(figure.Points);
            }
            else
            {
                Location = figure.Location;
                Width = figure.Width;
                Height = figure.Height;
            }
        }
        public virtual void Draw(Graphics g) { }
        public virtual double CalcArea() { return 0; }
        public virtual bool Contains(Point point)
        {
            bool isInsideX = _location.X <= point.X && point.X <= _location.X + _width;
            bool isInsideY = _location.Y <= point.Y && point.Y <= _location.Y + _height;

            return isInsideX && isInsideY;
        }
    }
}
