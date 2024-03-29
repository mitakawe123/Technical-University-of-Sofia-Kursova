﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace corel_draw.Figures
{
    internal class Polygon : Figure
    {
        private List<Point> _points;
        public override int Width
        {
            get => base.Width;
            set
            {
                for (int i = 0; i < _points.Count; i++)
                {
                    float scale = (_points[i].X - Location.X) / (float)base.Width;
                    int newX = (int)Math.Round(Location.X + value * scale, MidpointRounding.AwayFromZero);
                    _points[i] = new Point(newX, _points[i].Y);
                }
                base.Width = value;
            }
        }
        public override int Height
        {
            get => base.Height;
            set
            {
                for (int i = 0; i < _points.Count; i++)
                {
                    float scale = (_points[i].Y - Location.Y) / (float)base.Height;
                    int newY = (int)Math.Round(Location.Y + value * scale, MidpointRounding.AwayFromZero);
                    _points[i] = new Point(_points[i].X, newY);
                }
                base.Height = value;
            }
        }
        public List<Point> Points
        {
            get => _points;
            private set => _points = value;
        }

        public Polygon(List<Point> points) : base(GetLocationAndSize(points, out int width, out int height), width, height)
        {
            _points = points;
        }

        private static Point GetLocationAndSize(List<Point> points, out int width, out int height)
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

        public override Figure Clone() => new Polygon(_points);

        public override void CopyState(Figure figure)
        {
            base.CopyState(figure);
            if (figure is Polygon polygon)
                _points = new List<Point>(polygon._points);
        }

        public override void Move(Point newPoint) => Location = new Point(Location.X + newPoint.X, Location.Y + newPoint.Y);

        public override void Draw(Graphics g) => g.DrawPolygon(Pen, _points.ToArray());

        public override void Fill(Graphics g) => g.FillPolygon(Brush, _points.ToArray());

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
