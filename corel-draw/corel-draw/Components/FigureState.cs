using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corel_draw.Components
{
    internal class FigureState
    {
        public Point Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        //need for Polygon state
        public List<Point> Vertices { get; set; }

        public FigureState(Point position, int width, int height, Color color)
        {
            Position = position;
            Width = width;
            Height = height;
            Color = color;
        }

        public FigureState(List<Point> vertices, Color color)
        {
            Vertices = vertices;
            Color = color;
        }

        public FigureState CloneNonPolygonType()
        {
            return new FigureState(Position, Width, Height, Color);
        }

        public FigureState ClonePolygonType()
        {
            return new FigureState(new List<Point>(Vertices), Color);
        }
    }
}
