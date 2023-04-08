using corel_draw.Figures;
using corel_draw.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corel_draw.Components
{
    internal class EditCommand : ICommand
    {
        private readonly Figure _oldState;
        private readonly Figure _newState;
        private readonly Figure _initialState;

        public EditCommand(Figure oldState, Figure newState)
        {
            _oldState = oldState;
            _newState = newState;
            _initialState = oldState.Clone();
        }

        public void Do()
        {
            _initialState.Location = _oldState.Location;
            _initialState.Width = _oldState.Width;
            _initialState.Height = _oldState.Height;

            _oldState.Location = _newState.Location;
            _oldState.Width = _newState.Width;
            _oldState.Height = _newState.Height;

        }

        public void Undo()
        {
            _oldState.Location = _initialState.Location;
            _oldState.Width = _initialState.Width;
            _oldState.Height = _initialState.Height;

        }
    }

}
