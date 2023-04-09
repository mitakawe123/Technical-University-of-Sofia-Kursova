using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal class Polygon : Figure
    {
        public Polygon(List<Point> coordinates) : base(coordinates)
        {
        }
        public override void Move(Point newPoint)
        {
            Location = new Point(Location.X + newPoint.X, Location.Y + newPoint.Y);
        }

        public override Point Location
        {
            get => base.Location;
            set
            {
                Point delta = new Point(value.X - base.Location.X, value.Y - base.Location.Y);
                for (int i = 0; i < Points.Count; i++)
                {
                    Points[i] = new Point(Points[i].X + delta.X, Points[i].Y + delta.Y);
                }
                base.Location = value;
            }
        }

        public override void Draw(Graphics g)
        {
            g.DrawPolygon(new Pen(Color, 5), Points.ToArray());
        }

        //Jordan Curve Theorem
        public override bool Contains(Point point)
        {
            bool inside = false;
            int count = Points.Count;

            for (int i = 0, j = count - 1; i < count; j = i++)
            {
                if (((Points[i].Y > point.Y) != (Points[j].Y > point.Y)) &&
                    (point.X < (Points[j].X - Points[i].X) * (point.Y - Points[i].Y) / (Points[j].Y - Points[i].Y) + Points[i].X))
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
            int j = Points.Count - 1;
            for (int i = 0; i < Points.Count; i++)
            {
                area += (Points[j].X + Points[i].X) * (Points[j].Y - Points[i].Y);
                j = i;
            }

            return Math.Abs(area / 2);
        }
    }
}
