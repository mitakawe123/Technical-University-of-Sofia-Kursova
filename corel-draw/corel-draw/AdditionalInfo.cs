using corel_draw.Figures;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace corel_draw
{
    public partial class AdditionalInfo : Form
    {
        public AdditionalInfo(FigureInfo.FigureInfo figureInfo)
        {
            InitializeComponent();

            type_info.Text = figureInfo.TypeOfFigure;
            border_info.Text = figureInfo.BorderColor;
            fill_info.Text = figureInfo.FillColor;
            area_info.Text = figureInfo.AreaOfFigure;

            Dictionary<string, Figure> specialTags = new Dictionary<string, Figure>
            {
                {"Biggest Figure by Area", figureInfo.BiggestFigure },
                {"Smallest Figure by Area", figureInfo.SmallestFigure },
                {"First Created Figure", figureInfo.FirstFigure },
                {"Last Created Figure", figureInfo.LastFigure },
                {"Polygon with Most Sides", figureInfo.PolygonWithMostSides }
            };

            foreach (var kvp in specialTags)
            {
                if (kvp.Value != null)
                {
                    additional_info.Text += $"{kvp.Key}: {kvp.Value.GetType().Name}\n";
                }
            }
        }

        private void Close_btn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
