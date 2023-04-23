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
