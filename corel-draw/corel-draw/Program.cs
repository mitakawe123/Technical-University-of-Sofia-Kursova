using corel_draw.FactoryComponents;
using corel_draw.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Type[] figureFactoryTypes = typeof(FigureFactory).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(FigureFactory))).ToArray();


            var figureFactories = new List<FigureFactory>();
            foreach (var figureFactoryType in figureFactoryTypes)
            {
                var figureFactory = (FigureFactory)Activator.CreateInstance(figureFactoryType);
                figureFactories.Add(figureFactory);
            }

            Application.Run(new DrawingForm(figureFactories));
        }
    }
}
