﻿using corel_draw.Figures;
using System.Collections.Generic;
using CorelLibary;

namespace corel_draw.Components
{
    internal class AddCommand: ICommand
    {
        private readonly List<Figure> _figures;
        private readonly Figure _figure;

        public AddCommand(Figure figure, List<Figure> figures)
        {
            _figure = figure;
            _figures = figures;
        }

        public void Do() => _figures.Add(_figure);

        public void Undo() => _figures.Remove(_figure);
    }
}
