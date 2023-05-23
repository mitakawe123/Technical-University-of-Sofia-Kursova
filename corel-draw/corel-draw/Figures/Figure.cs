using Newtonsoft.Json;
using System.Drawing;

namespace corel_draw.Figures
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public abstract class Figure
    {
        private Point _location;
        private Color _fillColor;
        private Color _color;
        private int _width;
        private int _height;
        protected SolidBrush Brush { get; private set; }
        protected Pen Pen { get; private set; }
        public System.Drawing.Rectangle BoundingBox => new System.Drawing.Rectangle(Location.X, Location.Y, Width, Height);
        public bool ShowBoundingBox { get; set; }
        public Color Color 
        {
            get => _color;
            set 
            {
                _color = value;
                Pen = new Pen(_color,5);
            } 
        }
        public Color FillColor 
        {
            get => _fillColor; 
            set
            {
                _fillColor = value;
                Brush = new SolidBrush(_fillColor);
            }
        }
        public string Name { get; set; }
        public virtual Point Location { get => _location; set => _location = value; }
        public virtual int Width { get => _width; set => _width = value; }
        public virtual int Height { get => _height; set => _height = value; }

        public Figure(Point location, int width, int height)
        {
            _location = location;
            _width = width;
            _height = height;
            Color = Color.Black;
            FillColor = Color.White;
        }

        public virtual void Move(Point delta) => _location = new Point(_location.X + delta.X, _location.Y + delta.Y);

        public virtual void CopyState(Figure figure)
        {
            Location = figure.Location;
            Width = figure.Width;
            Height = figure.Height;
            Color = figure.Color;
            FillColor = figure.FillColor;
            Name = figure.Name;
        }

        public virtual bool Contains(Point point)
        {
            bool isInsideX = _location.X <= point.X && point.X <= _location.X + _width;
            bool isInsideY = _location.Y <= point.Y && point.Y <= _location.Y + _height;

            return isInsideX && isInsideY;
        }

        public abstract Figure Clone();

        public abstract void Draw(Graphics g);

        public abstract void Fill(Graphics g);

        public abstract double CalcArea();

        
    }
}
