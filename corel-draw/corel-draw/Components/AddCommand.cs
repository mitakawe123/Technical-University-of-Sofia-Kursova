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
    internal class AddCommand: ICommand
    {
        private readonly Figure _figure;
        private readonly List<Figure> _figures;

        public AddCommand(Figure figure, List<Figure> figures)
        {
            _figure = figure;
            _figures = figures;
        }

        public void Do()
        {
            _figures.Add(_figure);
        }

        public void Undo()
        {
            _figures.Remove(_figure);
        }
    }
}
