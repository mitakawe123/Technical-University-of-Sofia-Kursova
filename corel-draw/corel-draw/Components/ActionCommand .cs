using corel_draw.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corel_draw.Components
{
    internal class ActionCommand:ICommand
    {
        private Action _action;
        private Action _undoAction;
        private Stack<ICommand> _history = new Stack<ICommand>();

        public ActionCommand(Action action, Action undoAction)
        {
            _action = action;
            _undoAction = undoAction;
        }

        public void Execute()
        {
            _action();
        }

        public void Undo()
        {
            _undoAction();
        }
    }
}
