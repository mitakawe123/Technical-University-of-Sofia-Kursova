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
    internal class EditCommand : ICommand
    {
        private readonly Figure _oldState;
        private readonly Figure _newState;
        private readonly Figure _initialState;

        public EditCommand(Figure oldState, Figure newState)
        {
            _oldState = oldState;
            _newState = newState;
            _initialState = oldState.Clone();
        }

        public void Do()
        {
            if (_oldState is Polygon oldPolygon && _newState is Polygon newPolygon)
            {
                oldPolygon.Points = new List<Point>(newPolygon.Points);
            }
            else
            {
                _oldState.Location = _newState.Location;
                _oldState.Width = _newState.Width;
                _oldState.Height = _newState.Height;
            }
        }

        public void Undo()
        {
            if (_oldState is Polygon oldPolygon && _initialState is Polygon initialPolygon)
            {
                oldPolygon.Points = new List<Point>(initialPolygon.Points);
            }
            else
            {
                _oldState.Location = _initialState.Location;
                _oldState.Width = _initialState.Width;
                _oldState.Height = _initialState.Height;
            }
        }
    }
}
