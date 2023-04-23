using corel_draw.Figures;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace corel_draw
{
    public partial class AdditionalInfo : Form
    {
        private readonly Dictionary<string, Figure> _specialTags = new Dictionary<string, Figure>();
        public AdditionalInfo(Dictionary<string, Figure> _specialTags)
        {
            InitializeComponent();
            this._specialTags = _specialTags;
            foreach (var kvp in _specialTags)
            {
                additional_info.Text += $"{kvp.Key}: {kvp.Value.GetType().Name}\n";
            }
        }

        private void Close_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AdditionalInfo_Load(object sender, EventArgs e)
        {
        }
    }
}
