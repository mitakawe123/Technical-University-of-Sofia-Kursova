using corel_draw.Figures;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace corel_draw
{
    public partial class AdditionalInfo : Form
    {
        public AdditionalInfo(Dictionary<string, Figure> _specialTags,List<string> _defaultTags)
        {
            InitializeComponent();

            type_info.Text = _defaultTags[0];
            border_info.Text = _defaultTags[1];
            fill_info.Text = _defaultTags[2];
            area_info.Text = _defaultTags[3]; 
            
            foreach (var kvp in _specialTags)
            {
                if (_defaultTags[0] == kvp.Value.GetType().Name)
                    additional_info.Text += $"{kvp.Key}: {kvp.Value.GetType().Name}\n";
            }
        }

        private void Close_btn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
