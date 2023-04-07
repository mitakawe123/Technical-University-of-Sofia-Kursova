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
        private readonly Figure figure;
        private readonly List<Figure> _figures;
        private readonly Color _color;
        public ColorCommand(Figure figure, List<Figure> figures,Color color)
        {
            this.figure = figure;
            _figures = figures;
            this._color = color;
        }

        public void Do()
        {
            
        }

        public void Undo()
        {
            
        }
    }
}
