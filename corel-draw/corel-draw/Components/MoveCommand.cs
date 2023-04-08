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
    internal class MoveCommand:ICommand
    {
        private readonly Figure _figure;
        private readonly Point _delta;
        private readonly Point _initialPosition;
        private Point _finalPosition;

        public MoveCommand(Figure figure, Point delta,Point initialLocation)
        {
            _figure = figure;
            _delta = delta; 
            _initialPosition = initialLocation;
        }

        public void Do()
        {
            _figure.Move(_delta); 
            _finalPosition = _figure.Location;
        }

        public void Undo()
        {
            _figure.Location = _initialPosition;
        }
        public void Redo()
        {
            _figure.Location = _finalPosition;
        }
    }
}
