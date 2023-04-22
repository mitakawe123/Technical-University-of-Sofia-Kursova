using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw
{
    public partial class AdditionalInfo : Form
    {
        public string BiggestFigure { get; set; }
        public AdditionalInfo()
        {
            InitializeComponent();
        }

        private void Close_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AdditionalInfo_Load(object sender, EventArgs e)
        {
            additional_info.Text = BiggestFigure;
        }
    }
}
