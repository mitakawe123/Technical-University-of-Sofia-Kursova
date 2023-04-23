using corel_draw.Figures;
using System.Drawing;
using CorelLibary;

namespace corel_draw.Components
{
    public class MoveCommand : ICommand
    {
        private readonly Figure _figure;
        private readonly Point _oldPosition;
        private readonly Point _newPosition;

        public MoveCommand(Figure figure, Point newPosition,Point oldPosition)
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
