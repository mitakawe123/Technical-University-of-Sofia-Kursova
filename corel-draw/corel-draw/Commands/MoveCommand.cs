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

        public MoveCommand(Figure figure, Point oldPosition, Point newPosition)
        {
            _figure = figure;
            _oldPosition = oldPosition;
            _newPosition = newPosition;
        }

        public void Do()
        {
            _figure.Location = _newPosition;
        }

        public void Undo()
        {
            _figure.Location = _oldPosition;
        }
    }
}
