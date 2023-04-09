using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace corel_draw.Figures
{
    internal class Polygon : Figure
    {
        private List<Point> _points;

        public List<Point> Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(new
            {
                Name,
                Points = _points,
                Color,
            });
        }

        public Polygon(List<Point> coordinates) : base()
        {
            _points = coordinates;
            Color = Color.Black;
        }

        public override Point Location
        {
            get => base.Location;
            set
            {
                Point delta = new Point(value.X - base.Location.X, value.Y - base.Location.Y);
                for (int i = 0; i < _points.Count; i++)
                    _points[i] = new Point(_points[i].X + delta.X, _points[i].Y + delta.Y);

                base.Location = value;
            }
        }

        public override Figure Clone()
        {
            return new Polygon(_points);
        }

        public override void CopyState(Figure figure)
        {
            base.CopyState(figure);
            if (figure is Polygon polygon)
            {
                _points = new List<Point>(polygon._points);
            }
        }

        public override void Move(Point newPoint)
        {
            Location = new Point(Location.X + newPoint.X, Location.Y + newPoint.Y);
        }

        public override void Draw(Graphics g)
        {
            g.DrawPolygon(new Pen(Color, 5), _points.ToArray());
        }

        //Jordan Curve Theorem
        public override bool Contains(Point point)
        {
            bool inside = false;
            int count = _points.Count;

            for (int i = 0, j = count - 1; i < count; j = i++)
            {
                if (((_points[i].Y > point.Y) != (_points[j].Y > point.Y)) &&
                    (point.X < (_points[j].X - _points[i].X) * (point.Y - _points[i].Y) / (_points[j].Y - _points[i].Y) + _points[i].X))
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        public override double CalcArea()
        {
            double area = 0;

            //Shoelace formula
            int j = _points.Count - 1;
            for (int i = 0; i < _points.Count; i++)
            {
                area += (_points[j].X + _points[i].X) * (_points[j].Y - _points[i].Y);
                j = i;
            }

            return Math.Abs(area / 2);
        }
    }
}
