using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corel_draw.AddedLogic
{
    internal class ActionsHistory
    {
        private List<Action> undoList;
        private List<Action> redoList;

        public ActionsHistory()
        {
            undoList = new List<Action>();
            redoList = new List<Action>();
        }

        public void AddAction(Action action)
        {
            undoList.Add(action);
            redoList.Clear();
        }

        public void Undo()
        {
            if (undoList.Count > 0)
            {
                Action lastAction = undoList[undoList.Count - 1];
                undoList.RemoveAt(undoList.Count - 1);
                lastAction.Invoke();
                redoList.Add(lastAction);
            }
        }

        public void Redo()
        {
            if (redoList.Count > 0)
            {
                Action lastAction = redoList[redoList.Count - 1];
                redoList.RemoveAt(redoList.Count - 1);
                lastAction.Invoke();
                undoList.Add(lastAction);
            }
        }

        public void Clear()
        {
            undoList.Clear();
            redoList.Clear();
        }

        public bool CanUndo()
        {
            return undoList.Count > 0;
        }

        public bool CanRedo()
        {
            return redoList.Count > 0;
        }
    }
}
