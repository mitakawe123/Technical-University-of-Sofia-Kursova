using System;
using System.Collections.Generic;
using System.Windows.Forms;
using corel_draw.Interfaces;

namespace corel_draw.Components
{
    internal class CommandManager
    {
        private readonly List<ICommand> commandHistory = new List<ICommand>();
        private int commandIndex = -1;
        private readonly ListBox actionList;

        public CommandManager(ListBox actionList)
        {
            this.actionList = actionList;
        }

        public void AddCommand(ICommand command)
        {
            commandHistory.RemoveRange(commandIndex + 1, commandHistory.Count - commandIndex - 1);
            commandHistory.Add(command);
            command.Do();
            commandIndex = commandHistory.Count - 1;
            actionList.Items.Add(GetCommandDescription(command));
        }

        public void Undo()
        {
            if (commandIndex >= 0)
            {
                ICommand command = commandHistory[commandIndex];
                command.Undo();
                commandIndex--;
                actionList.Items.RemoveAt(actionList.Items.Count - 1);
            }
        }

        public void Redo()
        {
            if (commandIndex < commandHistory.Count - 1)
            {
                commandIndex++;
                ICommand command = commandHistory[commandIndex];
                command.Redo();
                actionList.Items.Add(GetCommandDescription(command));
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

        private string GetCommandDescription(ICommand command)
        {
            switch (command)
            {
                case MoveCommand moveCommand:
                    return moveCommand.GetDescription();
                case AddCommand addCommand:
                    return addCommand.GetDescription();
                case DeleteCommand deleteCommand:
                    return deleteCommand.GetDescription();
                case EditCommand editCommand:
                    return editCommand.GetDescription();
                case ColorCommand colorCommand:
                    return colorCommand.GetDescription();
                case LoadCommand loadCommand:
                    return loadCommand.GetDescription();
                default:
                    return "";
            }
        }
    }
}
