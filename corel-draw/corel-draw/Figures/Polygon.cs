using corel_draw.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

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

        public Polygon(List<Point> points) :base(GetLocationAndSize(points, out int width,out int height),width, height)
        {
            _points = points;
            Color = Color.Black;
        }

        private static Point GetLocationAndSize(List<Point> points, out int width,out int height)
        {
            int minX = points[0].X;
            int maxX = points[0].X;
            int minY = points[0].Y;
            int maxY = points[0].Y;
            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].X < minX) minX = points[i].X;
                if (points[i].X > maxX) maxX = points[i].X;
                if (points[i].Y < minY) minY = points[i].Y;
                if (points[i].Y > maxY) maxY = points[i].Y;
            }
            width = maxX - minX;
            height = maxY - minY;
            return new Point(minX, minY);
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
