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
        private readonly List<Point> _clickedPoints = new List<Point>();
        private Polygon _polygon;
        private readonly Pen _penDashed = new Pen(Color.Black, 5) { DashStyle = DashStyle.Dash };
       
        private bool _isPolygonFinishedDrawing = false;
        private bool _isDrawing;
        private bool _isDragging;

        private Point _startPoint;
        private Point _endPoint;
        private Point _lastPoint;

        private int _selectedPointIndex;
        public override void BeginCreateFigure()
        {
            _isPolygonFinishedDrawing = false;
            _clickedPoints.Clear();
            _isDrawing = false;
            _isDragging = false;
            _selectedPointIndex = -1;
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
            if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < _clickedPoints.Count; i++)
                {
                    if (Math.Abs(_clickedPoints[i].X - e.X) <= 10 && Math.Abs(_clickedPoints[i].Y - e.Y) <= 10)
                    {
                        _selectedPointIndex = i;
                        _isDragging = true; 
                        break;
                    }
                }
            }
            else
            {
                _isDragging = false;
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            //_startPoint = e.Location;
            if (_isDragging && _selectedPointIndex < _clickedPoints.Count)
            {
                _clickedPoints[_selectedPointIndex] = e.Location;
            }
            _isDragging = false;
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
                        if (_isDragging && i == _selectedPointIndex)
                        {
                            g.FillEllipse(Brushes.Red, _clickedPoints[i].X - 8, _clickedPoints[i].Y - 8, 16, 16);
                        }
                        else
                        {
                            g.FillEllipse(Brushes.Black, _clickedPoints[i].X - 4, _clickedPoints[i].Y - 4, 8, 8);
                        }
                    }
                    g.FillPath(Brushes.Black, path);
                }
            }
            else
            {
                g.DrawLines(_defaultPen, _clickedPoints.ToArray());
                g.DrawLine(_defaultPen, _clickedPoints[_clickedPoints.Count - 1], _clickedPoints[0]);

                _polygon = new Polygon(_clickedPoints.ToList());
                OnFinished(_polygon);
                _clickedPoints.Clear();
            }
        }
    }
}
