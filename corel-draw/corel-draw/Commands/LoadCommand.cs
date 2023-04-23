using corel_draw.Figures;
using CorelLibary;
using System.Collections.Generic;

namespace corel_draw.Components
{
    internal class LoadCommand : ICommand
    {
        private readonly List<Figure> _oldFigures;
        private readonly List<Figure> _newFigures;

        public LoadCommand(List<Figure> oldFigures, List<Figure> newFigures)
        {
            _oldFigures = oldFigures;
            _newFigures = newFigures;
        }

        public void Do()
        {
            _oldFigures.AddRange(_newFigures); 
        }

        public void Undo()
        {
            foreach (Figure figure in _newFigures)
            {
                _oldFigures.Remove(figure); 
            }
        }
    }
}
