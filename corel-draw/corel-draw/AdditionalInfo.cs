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

            type_info.Text = figureInfo.DefaultProps[0];
            border_info.Text = figureInfo.DefaultProps[1];
            fill_info.Text = figureInfo.DefaultProps[2];
            area_info.Text = figureInfo.DefaultProps[3];

            foreach (var prop in figureInfo.SpecialProps)
                if (prop != null)
                    additional_info.Text += $"{prop}\n";
        }

        private void Close_btn_Click(object sender, EventArgs e) => Close();
    }
}
