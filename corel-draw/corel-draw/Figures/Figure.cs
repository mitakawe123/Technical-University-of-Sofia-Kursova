using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw.Figures
{
    internal class Figure
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        private int sides;
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int Sides { get; set; }
        public bool isSelected { get; set; }

        public Figure(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Figure(float x, float y, float width, float height, int sides)
        {
            X = x;
            Y = y;
            Width = width;
            Sides = sides;
        }

        public virtual void DrawFigure(PaintEventArgs e, float x, float y, float width, float height) { }
        public virtual void DrawPolygon(PaintEventArgs e, float x, float y, float width, float height, int sides) { }

        public void Move(int deltaX, int deltaY)
        {
            // Update the position of the figure based on the given deltaX and deltaY values
            // For example:
            X += deltaX;
            Y += deltaY;
        }
    }
}
