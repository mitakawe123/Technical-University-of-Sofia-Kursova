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
    public partial class PolygonSides : Form
    {
        public int Sides { get; private set; }
        public bool isDrawing { get; set; }

        public PolygonSides()
        {
            InitializeComponent(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Polygon_Sides.Text, out int number))
            {
                if(number < 3)
                {
                    MessageBox.Show("Please enter a number above 2");
                    return;
                }
                Sides = number;
                isDrawing = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Please enter a valid number.");
        }
    }
}
