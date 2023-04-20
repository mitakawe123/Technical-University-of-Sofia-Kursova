using corel_draw.Figures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.FactoryComponents
{
    internal class PolygonFactory : FigureFactory
    {
        private Point[] _points;
        private Polygon _polygon;

        public override void BeginCreateFigure()
        {
            _polygon = new Polygon();
        }
        public override void MouseDown(MouseEventArgs e)
        {
            if (_points == null)
            {
                _points = new Point[1];
                _points[0] = e.Location;
            }
            else
            {
                Point[] newPoints = new Point[_points.Length + 1];
                Array.Copy(_points, newPoints, _points.Length);
                newPoints[_points.Length] = e.Location;
                _points = newPoints;
            }
        }

        public override void MouseMove(MouseEventArgs e)
        {
            if (_points != null)
            {
                Point[] newPoints = new Point[_points.Length + 1];
                Array.Copy(_points, newPoints, _points.Length);
                newPoints[_points.Length] = e.Location;
                _polygon = new Polygon(newPoints.ToList());
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            if (_points != null && _points.Length >= 3)
            {
                _polygon = new Polygon(_points.ToList());
                Finished?.Invoke(_polygon);
            }
            _points = null;
        }
    }
}
