using corel_draw.Figures;
using System.Drawing;
using CorelLibary;

namespace corel_draw.Components
{
    public class MoveCommand : ICommand
    {
        private readonly Figure _figure;
        private readonly Point _delta;
        private readonly Point _initialPosition;
        private Point _finalPosition;

        public MoveCommand(Figure figure, Point delta, Point initialLocation)
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
