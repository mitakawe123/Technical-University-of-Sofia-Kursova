using corel_draw.Figures;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace corel_draw.FactoryComponents
{
    internal class PolygonFactory : FigureFactory
    {
        private readonly List<Point> _clickedPoints = new List<Point>();
        private Polygon _polygon;
        private readonly Pen _penDashed = new Pen(Color.Black, 5) { DashStyle = DashStyle.Dash };
       
        private bool _isPolygonFinishedDrawing = false;
        private bool _isDrawing = false;

        private Point _startPoint;
        private Point _endPoint;

        public override void BeginCreateFigure()
        {
            _polygon = new Polygon();
        }
        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _startPoint = e.Location;
                _isDrawing = true;
                _clickedPoints.Add(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                _isPolygonFinishedDrawing = true;
            }
        }
        public override void MouseMove(MouseEventArgs e)
        {
            _endPoint = e.Location;
        }

        public override void MouseUp(MouseEventArgs e)
        {
        }

        public override void Draw(Graphics g)
        {
            if (_isDrawing)
            {
                g.DrawLine(_penDashed, _startPoint, _endPoint);
            }
            if (!_isPolygonFinishedDrawing)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    for (int i = 0; i < _clickedPoints.Count; i++)
                    {
                        if (i < _clickedPoints.Count - 1)
                        {
                            g.DrawLine(_penDashed, _clickedPoints[i], _clickedPoints[i + 1]);
                        }
                        else
                        {
                            g.DrawLine(_penDashed, _clickedPoints[i], _clickedPoints[0]);
                        }
                        path.AddEllipse(_clickedPoints[i].X - 3, _clickedPoints[i].Y - 3, 6, 6);
                    }
                    g.FillPath(Brushes.Black, path);
                }
            }
            else
            {
                g.DrawLines(_defaultPen, _clickedPoints.ToArray());
                g.DrawLine(_defaultPen, _clickedPoints[_clickedPoints.Count - 1], _clickedPoints[0]);

                _polygon.Points = _clickedPoints.ToList();
                OnFinished(_polygon);
                _clickedPoints.Clear();
            }
        }
    }
}
