using corel_draw.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corel_draw.Components
{
    [Serializable]
    internal class DrawingData
    {
        public List<Figure> DrawnFigures { get; private set; }
    }
}
