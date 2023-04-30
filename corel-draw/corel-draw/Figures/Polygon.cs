using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace corel_draw.Figures
{
    internal class Polygon : Figure
    {
        private readonly Pen _dashPen = new Pen(Color.Blue, 10) { DashStyle = DashStyle.Dot };
        private System.Drawing.Rectangle _boundingRect;
        private List<Point> _points;
        
        public List<Point> Points 
        { 
            get => _points; 
            private set => _points = value; 
        }
        
        public Polygon(List<Point> points) : base(GetLocationAndSize(points, out int width,out int height),width, height)
        {
            _points = points;
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
        public void GetPolygonBounds(List<Point> polygon, out int minX, out int minY, out int maxX, out int maxY)
        {
            minX = polygon.Min(p => p.X);
            minY = polygon.Min(p => p.Y);
            maxX = polygon.Max(p => p.X);
            maxY = polygon.Max(p => p.Y);
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
        public override void Resize(int width,int height)
        {
            int currentWidth = Width;
            int currentHeight = Height;

            Width = width;
            Height = height;

            Point center = new Point(Location.X + currentWidth / 2, Location.Y + currentHeight / 2);
            for (int i = 0; i < _points.Count; i++)
            {
                int deltaX = (int)Math.Round((_points[i].X - center.X) * (double)width / currentWidth);
                int deltaY = (int)Math.Round((_points[i].Y - center.Y) * (double)height / currentHeight);
                _points[i] = new Point(center.X + deltaX, center.Y + deltaY);
            }
        }

        public override void Move(Point newPoint) => Location = new Point(Location.X + newPoint.X, Location.Y + newPoint.Y);

        public override void Draw(Graphics g)
        {
            g.DrawPolygon(Pen, _points.ToArray());
         
            if (ShowPolygonBoundingBox)
            {
                GetPolygonBounds(_points, out int minX, out int minY, out int maxX, out int maxY);
                _boundingRect = new System.Drawing.Rectangle(minX, minY, maxX - minX, maxY - minY);
                g.DrawRectangle(_dashPen, _boundingRect);
            }
        }

        public override bool IsInsideBoundingBox(Point point)
        {
            System.Drawing.Rectangle expandedRect = new System.Drawing.Rectangle(_boundingRect.X - 5, _boundingRect.Y - 5, _boundingRect.Width + 20, _boundingRect.Height + 20);
            return expandedRect.Contains(point);
        }

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
