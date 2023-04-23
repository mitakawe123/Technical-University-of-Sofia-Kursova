using System.Collections.Generic;

namespace CorelLibary
{
    public class CommandManager
    {
        private readonly List<ICommand> commandHistory = new List<ICommand>();
        public bool CanUndo => commandIndex >= 0;
        public bool CanRedo => commandIndex < commandHistory.Count - 1;

        private int commandIndex = -1;

        public void AddCommand(ICommand command)
        {
            commandHistory.Add(command);
            command.Do();
            commandIndex = commandHistory.Count - 1;
        }

        public void Undo()
        {
            if (CanUndo)
            {
                ICommand command = commandHistory[commandIndex];
                command.Undo();
                commandIndex--;
            }
        }

        public void Redo()
        {
            if (CanRedo)
            {
                commandIndex++;
                ICommand command = commandHistory[commandIndex];
                command.Redo();
            }
        }
    }
}
