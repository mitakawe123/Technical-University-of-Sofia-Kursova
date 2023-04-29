using corel_draw.Figures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace corel_draw.FactoryComponents
{
    internal class PolygonFactory : FigureFactory
    {
        private const int SELECTION_THRESHOLD = 20;

        private readonly Pen _penDashed = new Pen(Color.Black, 5) { DashStyle = DashStyle.Dash };
        private readonly List<Point> _clickedPoints = new List<Point>();
        private Polygon _polygon;

        private Point _startPoint;
        private Point _endPoint;
        
        private bool _isPolygonFinishedDrawing;
        private bool _dragginPoint;

        private int _selectedPointIndex;
        
        public override void BeginCreateFigure()
        {
            _isPolygonFinishedDrawing = false;
            _isDrawing = false;
            _isScrolling = false;
            _dragginPoint = false;
            _selectedPointIndex = -1;
            _clickedPoints.Clear();
        }

        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Control.ModifierKeys != Keys.Control)
                {
                    _startPoint = e.Location;
                    _isDrawing = true;
                    _clickedPoints.Add(e.Location);
                    return;
                }
                
                for (int i = 0; i < _clickedPoints.Count; i++)
                {
                    if (Math.Abs(e.Location.X - _clickedPoints[i].X) < SELECTION_THRESHOLD &&
                        Math.Abs(e.Location.Y - _clickedPoints[i].Y) < SELECTION_THRESHOLD)
                    {
                        _selectedPointIndex = i;
                        _dragginPoint = true;
                        break;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
                _isPolygonFinishedDrawing = true;
        }

        public override void MouseMove(MouseEventArgs e)
        {
            _endPoint = e.Location;
            if (_dragginPoint) 
                _clickedPoints[_selectedPointIndex] = e.Location;
        }

        public override void MouseUp(MouseEventArgs e)
        {
            _startPoint = e.Location;
            _dragginPoint = false;
        }

        public override void MouseWheel(MouseEventArgs e, Figure currentFigure)
        {
            _isScrolling = true;
            if (e.Delta > 0)
                currentFigure.Resize(currentFigure.Width + 10,currentFigure.Height + 10);
            else
                currentFigure.Resize(currentFigure.Width - 10, currentFigure.Height - 10);
        }

        public override void Draw(Graphics g)
        {
            if (_isDrawing && !_isScrolling && !_dragginPoint)
                g.DrawLine(_penDashed, _startPoint, _endPoint);
            
            if (!_isPolygonFinishedDrawing)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    for (int i = 0; i < _clickedPoints.Count; i++)  
                    {
                        if (i < _clickedPoints.Count - 1)
                            g.DrawLine(_penDashed, _clickedPoints[i], _clickedPoints[i + 1]);
                        else 
                            g.DrawLine(_penDashed, _clickedPoints[i], _clickedPoints[0]);

                        if (i == _selectedPointIndex)
                            g.FillEllipse(Brushes.Red, _clickedPoints[i].X - 8, _clickedPoints[i].Y - 8, 16, 16);
                        else
                            g.FillEllipse(Brushes.Black, _clickedPoints[i].X - 4, _clickedPoints[i].Y - 4, 8, 8);
                    }
                    g.FillPath(Brushes.Black, path);
                }
            }
            else if(!_isScrolling)
            {
                g.DrawLines(_defaultPen, _clickedPoints.ToArray());
                g.DrawLine(_defaultPen, _clickedPoints[_clickedPoints.Count - 1], _clickedPoints[0]);

                _polygon = new Polygon(_clickedPoints.ToList());
                OnFinished(_polygon);
            }
        }
    }
}
