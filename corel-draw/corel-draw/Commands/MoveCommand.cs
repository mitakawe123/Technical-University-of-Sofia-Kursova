using corel_draw.Figures;
using System.Drawing;
using CorelLibary;

namespace corel_draw.Components
{
    public class MoveCommand : ICommand
    {
        private readonly Figure _figure;
        private readonly Point _newPosition;
        private Point _oldPosition;

        public MoveCommand(Figure figure, Point newPosition)
        {
            _figure = figure;
            _newPosition = newPosition;
        }

        public void Do()
        {
            _oldPosition = _figure.Location;
            _figure.Location = _newPosition;
        }

        public void Undo()
        {
            _figure.Location = _oldPosition;
        }
    }
}
