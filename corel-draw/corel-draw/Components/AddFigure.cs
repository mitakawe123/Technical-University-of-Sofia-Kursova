using corel_draw.Figures;
using corel_draw.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Components
{
    internal class AddFigure: ICommand
    {
        private readonly Figure figure;
        private readonly List<Figure> _figures;

        public AddFigure(Figure figure, List<Figure> figures)
        {
            this.figure = figure;
            _figures = figures;
        }

        public void Do()
        {
            _figures.Add(figure);
        }

        public void Undo()
        {
            _figures.Remove(figure);
        }
    }
}
