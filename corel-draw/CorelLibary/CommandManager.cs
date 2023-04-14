using System.Collections.Generic;

namespace CorelLibary
{
    public class CommandManager
    {
        private readonly List<ICommand> commandHistory = new List<ICommand>();
        private int commandIndex = -1;
        public CommandManager()
        {
        }

        public void AddCommand(ICommand command)
        {
            commandHistory.RemoveRange(commandIndex + 1, commandHistory.Count - commandIndex - 1);
            commandHistory.Add(command);
            command.Do();
            commandIndex = commandHistory.Count - 1;
        }

        public void Undo()
        {
            if (commandIndex >= 0)
            {
                ICommand command = commandHistory[commandIndex];
                command.Undo();
                commandIndex--;
            }
        }

        public void Redo()
        {
            if (commandIndex < commandHistory.Count - 1)
            {
                commandIndex++;
                ICommand command = commandHistory[commandIndex];
                command.Redo();
            }
        }

        public bool CanUndo()
        {
            return commandIndex >= 0;
        }

        public bool CanRedo()
        {
            return commandIndex < commandHistory.Count - 1;
        }
    }
}
