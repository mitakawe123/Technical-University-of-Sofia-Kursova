using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corel_draw.Interfaces
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
