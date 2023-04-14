using System.Collections.Generic;

namespace CorelLibary
{
    public class CommandManager
    {
        private readonly List<ICommand> commandHistory = new List<ICommand>();
        private readonly Stack<ICommand> redoStack = new Stack<ICommand>();

        private int commandIndex = -1;
        public CommandManager()
        {
        }

        public void AddCommand(ICommand command)
        {
            // If we're not at the end of the command history, clear the redo stack
            if (commandIndex < commandHistory.Count - 1)
            {
                redoStack.Clear();
            }
            //commandHistory.RemoveRange(commandIndex + 1, commandHistory.Count - commandIndex - 1);
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
