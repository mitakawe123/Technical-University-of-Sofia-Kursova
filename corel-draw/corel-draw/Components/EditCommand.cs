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
            _oldState.CopyState(_newState);
        }

        public void Undo()
        {
            _oldState.CopyState(_initialState);
        }

        public void Redo()
        {
            _oldState.CopyState(_newState);
        }

        public string GetDescription()
        {
            return $"Edit {_oldState.GetType().Name} with new area of {_newState.CalcArea():F2}";
        }
    }
}
