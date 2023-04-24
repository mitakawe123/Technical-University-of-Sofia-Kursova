using corel_draw.Figures;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace corel_draw.FactoryComponents
{
    public abstract class FigureFactory
    {
        //Shift -> inser point
        //Ctrl -> move point
        protected readonly Pen _defaultPen = new Pen(Color.Black, 2f);

        private event Action<Figure> _finished;

        public event Action<Figure> Finished
        {
            add { _finished += value; }
            remove { _finished -= value; }
        }

        public abstract void BeginCreateFigure();
        public abstract void MouseDown(MouseEventArgs e);
        public abstract void MouseMove(MouseEventArgs e);
        public abstract void MouseUp(MouseEventArgs e);
        public abstract void Draw(Graphics g);

        public void OnFinished(Figure figure)
        {
            _finished?.Invoke(figure);
        }
    }
}
