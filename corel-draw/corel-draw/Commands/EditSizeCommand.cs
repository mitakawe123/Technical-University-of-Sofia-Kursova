using corel_draw.Figures;
using CorelLibary;

namespace corel_draw.Components
{
    internal class EditSizeCommand : ICommand
    {
        private readonly Figure _oldState;
        private readonly Figure _newState;
        private readonly Figure _initialState;

        public EditSizeCommand(Figure oldState, Figure newState)
        {
            _oldState = oldState;
            _newState = newState;
            _initialState = oldState.Clone();
        }

        public void Do() => _oldState.CopyState(_newState);

        public void Undo() => _oldState.CopyState(_initialState);
    }
}
