using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorelLibary
{
    public class Figure1
    {
        private string _name;
        //public string Name { get; set; }
        public Figure1 (string name)
        {
            _name = name;
        }

        public string CallMain()
        {
            return _name;
        }
    }
}
