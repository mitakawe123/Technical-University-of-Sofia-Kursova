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
    internal class ColorCommand:ICommand
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

        public void Redo()
        {
            _figure.Color = _newColor;
        }
    }
}
