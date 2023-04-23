using corel_draw.Figures;
using System.Drawing;
using CorelLibary;
namespace corel_draw.Components
{
    internal class ColorCommand : ICommand
    {
        private readonly Figure _figure;
        private readonly Color _oldColor;
        private readonly Color _newColor;

        public ColorCommand(Figure figure, Color oldColor, Color newColor)
        {
            _figure = figure;
            _oldColor = oldColor;
            _newColor = newColor;
        }

        public void Do()
        {
            _figure.Color = _newColor;
        }

        public void Undo()
        {
            _figure.Color = _oldColor;
        }
    }
}
