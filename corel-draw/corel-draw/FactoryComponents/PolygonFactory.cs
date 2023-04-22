using corel_draw.Figures;
using System.Collections.Generic;
using System.Diagnostics;
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
        private bool _isPolygonFinishedDrawing;
        private readonly Pen _pen = new Pen(Color.Black, 2f);
        private readonly Pen _penDashed = new Pen(Color.Black, 5) { DashStyle = DashStyle.Dash };

        public override void BeginCreateFigure()
        {
            _polygon = new Polygon();
            _isPolygonFinishedDrawing = false;
        }
        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _clickedPoints.Add(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                _isPolygonFinishedDrawing = true;
            }
        }
        public override void MouseMove(MouseEventArgs e)
        {                
        }

        public override void MouseUp(MouseEventArgs e)
        {
        }

        public override void Draw(Graphics g)
        {
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
                        path.AddEllipse(_clickedPoints[i].X - 3, _clickedPoints[i].Y - 3, 6, 6);
                    }
                    g.FillPath(Brushes.Black, path);
                }
            }
            else
            {
                g.DrawLines(_pen, _clickedPoints.ToArray());
                g.DrawLine(_pen, _clickedPoints[_clickedPoints.Count - 1], _clickedPoints[0]);

                _polygon.Points = _clickedPoints.ToList();
                OnFinished(_polygon);
                _clickedPoints.Clear();
            }
        }
    }
}
