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
        private readonly Point _oldPosition;
        private readonly Point _newPosition;

        public MoveCommand(Figure figure, Point oldPosition, Point newPosition)
        {
            _figure = figure;
            _oldPosition = oldPosition;
            _newPosition = newPosition;
        }

        public void Do()
        {
            _figure.Move(_newPosition);
        }

        public void Undo()
        {
            _figure.Move(_oldPosition);
        }
    }
}
