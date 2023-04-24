using corel_draw.Figures;
using System.Collections.Generic;
using System.Linq;

namespace corel_draw.FigureInfo
{
    public class FigureInfo
    {
        public List<Figure> DrawnFigures { get; private set; }
        public Figure CurrentFigure { get; private set; }
        public Figure BiggestFigure { get; private set; }
        public Figure SmallestFigure { get; private set; }
        public Figure FirstFigure { get; private set; }
        public Figure LastFigure { get; private set; }
        public Figure PolygonWithMostSides { get; private set; }
        public string TypeOfFigure { get; private set; }
        public string BorderColor { get; private set; }
        public string FillColor { get; private set; }
        public string AreaOfFigure {get;private set;}

        public FigureInfo(List<Figure> drawnFigures,Figure currentFigure)
        {
            DrawnFigures = drawnFigures;
            CurrentFigure = currentFigure;

            BiggestFigure = drawnFigures.OrderByDescending(f => f.CalcArea()).First();
            SmallestFigure = drawnFigures.OrderBy(f => f.CalcArea()).First();
            FirstFigure = drawnFigures.First();
            LastFigure = drawnFigures.Last();
            PolygonWithMostSides = drawnFigures.OfType<Polygon>().OrderByDescending(p => p.Points.Count).FirstOrDefault();

            TypeOfFigure = currentFigure.GetType().Name;
            BorderColor = currentFigure.Color.Name;
            FillColor = currentFigure.FillColor.Name;
            AreaOfFigure = currentFigure.CalcArea().ToString("F2");
        }
    }
}
