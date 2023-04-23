using System.Collections.Generic;

namespace CorelLibary
{
    public class CommandManager
    {
        private readonly List<ICommand> commandHistory = new List<ICommand>();
       // private readonly Stack<ICommand> redoStack = new Stack<ICommand>();
        public bool CanUndo => commandIndex >= 0;
        public bool CanRedo => commandIndex < commandHistory.Count - 1;

        private int commandIndex = -1;
        public CommandManager()
        {
        }

        public void AddCommand(ICommand command)
        {
            /*if (commandIndex < commandHistory.Count - 1)
            {
                redoStack.Clear();
            }*/
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
    }
}
