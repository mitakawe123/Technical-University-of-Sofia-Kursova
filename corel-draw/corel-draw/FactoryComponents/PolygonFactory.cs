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
        private List<Point> _clickedPoints;
        private Polygon _polygon;
        private PolygonSides _sidesForm;
        private int _sides;

        public override void BeginCreateFigure()
        {
            _polygon = new Polygon();
            _clickedPoints = new List<Point>();
            _sidesForm = new PolygonSides();
            DialogResult result = _sidesForm.ShowDialog();
            if(result == DialogResult.OK) 
            {
                _sides = _sidesForm.Sides;
            }
        }
        public override void MouseDown(MouseEventArgs e)
        {
            _clickedPoints.Add(e.Location);
            
        }
        public override void MouseMove(MouseEventArgs e)
        {
        }

        public override void MouseUp(MouseEventArgs e)
        {
            if (_clickedPoints.Count > 0)
            {
                _clickedPoints[_clickedPoints.Count - 1] = e.Location;
            }
        }

        public override void Draw(Graphics g)
        {
            if (_clickedPoints.Count == _sides)
            {
                using (Pen pen = new Pen(Color.Black, 2f))
                {
                    if (_clickedPoints.Count > 1)
                    {
                        g.DrawLines(pen, _clickedPoints.ToArray());
                        g.DrawLine(pen, _clickedPoints[_clickedPoints.Count - 1], _clickedPoints[0]);
                    }
                }

                using (GraphicsPath path = new GraphicsPath())
                {
                    foreach (Point p in _clickedPoints)
                    {
                        path.AddEllipse(p.X - 3, p.Y - 3, 6, 6);
                    }
                    g.FillPath(Brushes.Black, path);
                }

                _polygon.Points = _clickedPoints.ToList();
                Finished?.Invoke(_polygon);
                _clickedPoints.Clear();
            }
            else if (_clickedPoints.Count != _sides && _clickedPoints.Count > 1)
            {
                g.DrawLine(new Pen(Color.Black, 2f), _clickedPoints[_clickedPoints.Count - 2], _clickedPoints[_clickedPoints.Count - 1]);
            }
        }
    }
}
