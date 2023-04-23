using corel_draw.Figures;
using CorelLibary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corel_draw.Components
{
    internal class FillCommand : ICommand
    {
        private readonly Figure _figure;
        private readonly Color _oldFilling;
        private readonly Color _newFilling;

        public FillCommand(Figure figure, Color oldColor, Color newColor)
        {
            _figure = figure;
            _oldFilling = oldColor;
            _newFilling = newColor;
        }

        public void Do()
        {
            _figure.FillColor = _newFilling;
        }

        public void Undo()
        {
            _figure.FillColor = _oldFilling;
        }
    }
}
