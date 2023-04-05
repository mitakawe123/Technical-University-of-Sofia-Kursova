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
        public PolygonSides()
        {
            InitializeComponent(); 
        }

        private void PolygonSides_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Polygon_Sides.Text, out int number))
            {
                Sides = number;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Please enter a valid number.");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
