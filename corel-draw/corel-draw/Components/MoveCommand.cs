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

        public MoveCommand(Figure figure, Point delta)
        {
            _figure = figure;
            _delta = delta;
        }

        public void Do()
        {
            _figure.Move(_delta);
        }

        public void Undo()
        {
            _figure.Move(new Point(-_delta.X, -_delta.Y));
        }
    }
}
