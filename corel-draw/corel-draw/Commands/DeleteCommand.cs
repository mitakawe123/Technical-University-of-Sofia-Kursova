﻿using corel_draw.Figures;
using CorelLibary;
using System.Collections.Generic;

namespace corel_draw.Components
{
    internal class DeleteCommand : ICommand
    {
        private readonly List<Figure> _figures;
        private readonly Figure _figure;

        public DeleteCommand(Figure figure, List<Figure> figures)
        {
            _figure = figure;
            _figures = figures;
        }

        public void Do() => _figures.Remove(_figure);

        public void Undo() => _figures.Add(_figure);
    }
}
