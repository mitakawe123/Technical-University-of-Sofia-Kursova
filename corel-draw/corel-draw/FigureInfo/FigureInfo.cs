using corel_draw.Figures;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace corel_draw.FigureInfo
{
    public class FigureInfo
    {
        public List<string> SpecialProps { get; private set; }
        public List<string> DefaultProps{ get; private set; }

        private readonly Figure _biggestFigure;
        private readonly Figure _smallestFigure;
        private readonly Figure _firstFigure;
        private readonly Figure _lastFigure;
        private readonly Figure _polygonWithMostSides;

        public FigureInfo(List<Figure> drawnFigures,Figure currentFigure)
        {
            SpecialProps = new List<string>();
            DefaultProps = new List<string>()
            {
                currentFigure.GetType().Name,
                currentFigure.Color.Name,
                currentFigure.FillColor.Name,
                currentFigure.CalcArea().ToString("F2")
            };  

            _biggestFigure = drawnFigures.OrderByDescending(f => f.CalcArea()).First();
            _smallestFigure = drawnFigures.OrderBy(f => f.CalcArea()).First();
            _firstFigure = drawnFigures.First();
            _lastFigure = drawnFigures.Last();
            _polygonWithMostSides = drawnFigures.OfType<Polygon>().OrderByDescending(p => p.Points.Count).FirstOrDefault();

            if (currentFigure == _biggestFigure)
                SpecialProps.Add("Biggest Figure by Area");
            if (currentFigure == _smallestFigure) 
                SpecialProps.Add("Smallest Figure by Area");
            if (currentFigure == _firstFigure)
                SpecialProps.Add("First Created Figure");
            if (currentFigure == _lastFigure) 
                SpecialProps.Add("Last Created Figure");
            if (currentFigure == _polygonWithMostSides) 
                SpecialProps.Add("Polygon with Most Sides");
        }
    }
}
