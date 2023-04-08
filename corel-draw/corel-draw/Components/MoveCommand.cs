using corel_draw.Figures;
using corel_draw.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corel_draw.Components
{
    internal class MoveCommand : ICommand
    {
        private readonly Figure _figure;
        private readonly Point _delta;
        private readonly Point _initialPosition;
        private Point _finalPosition;
        private List<Point> _initialPoints;
        private List<Point> _initialPointPositions;

        public MoveCommand(Figure figure, Point delta, Point initialLocation)
        {
            _figure = figure;
            _delta = delta;
            _initialPosition = initialLocation;
            if (_figure is Polygon polygon)
            {
                _initialPoints = polygon.Points.Select(p => new Point(p.X, p.Y)).ToList();
                _initialPointPositions = polygon.Points.Select(p => new Point(p.X, p.Y)).ToList();
            }
        }

        public void Do()
        {
            if (_figure is Polygon polygon)
            {
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Point(polygon.Points[i].X + _delta.X, polygon.Points[i].Y + _delta.Y);
                    _initialPointPositions[i] = new Point(_initialPoints[i].X + _delta.X, _initialPoints[i].Y + _delta.Y);
                }
            }
            else
            {
                _figure.Move(_delta);
                _finalPosition = _figure.Location;
            }
        }

        public void Undo()
        {
            if (_figure is Polygon polygon)
            {
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Point(_initialPointPositions[i].X, _initialPointPositions[i].Y);
                }
            }
            else
            {
                _figure.Location = _initialPosition;
            }
        }

        public void Redo()
        {
            if (_figure is Polygon polygon)
            {
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Point(_initialPointPositions[i].X + _delta.X, _initialPointPositions[i].Y + _delta.Y);
                }
            }
            else
            {
                _figure.Location = _finalPosition;
            }
        }
    }

}
