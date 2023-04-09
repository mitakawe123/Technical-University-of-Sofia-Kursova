using System;
using System.Windows.Forms;

namespace corel_draw
{
    public partial class PolygonSides : Form
    {
        public int Sides { get; private set; }
        public bool IsDrawing { get; set; }

        public PolygonSides()
        {
            InitializeComponent(); 
        }

        private void DrawPolygon_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Polygon_Sides.Text, out int number))
            {
                if(number < 3)
                {
                    MessageBox.Show("Please enter a number above 2");
                    return;
                }
                Sides = number;
                IsDrawing = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Please enter a valid number.");
        }
    }
}
