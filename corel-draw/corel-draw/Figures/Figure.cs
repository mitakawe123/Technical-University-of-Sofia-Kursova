using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace corel_draw.Figures
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    abstract class Figure
    {
        private Point _location;
        private int _width;
        private int _height;

        public Color Color { get; set; }
        public string Name { get; set; }
        public virtual Point Location { get => _location; set => _location = value; }
        public int Width { get => _width; set => _width = value; }

        public int Height { get => _height; set => _height = value; }

        protected Figure()
        {
            //need empty constructor for deserialize the data in the json file
        }
        
        public Figure(Point location, int width, int height)
        {
            _location = location;
            _width = width;
            _height = height;
            Color = Color.Black;
        }

        public virtual void Move(Point delta)
        {
            _location = new Point(_location.X + delta.X, _location.Y + delta.Y);
        }

        public abstract Figure Clone();

        public virtual void CopyState(Figure figure)
        {
            Location = figure.Location;
            Width = figure.Width;
            Height = figure.Height;
            Color = figure.Color;
            Name = figure.Name;
        }

        public abstract void Draw(Graphics g);

        public abstract double CalcArea();

        public virtual bool Contains(Point point)
        {
            bool isInsideX = _location.X <= point.X && point.X <= _location.X + _width;
            bool isInsideY = _location.Y <= point.Y && point.Y <= _location.Y + _height;

            return isInsideX && isInsideY;
        }
    }
}
