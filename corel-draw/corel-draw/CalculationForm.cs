using corel_draw.Figures;
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
    public partial class CalculationForm : Form
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width_Value { get; set; }
        public int Height_Value { get; set; }
        public int Sides_Value { get; set; }
        public CalculationForm(Type type)
        {
            InitializeComponent();
            Toggle_Height_Input_Visibility(type != typeof(Square));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(X_Input.Text))
            {
                MessageBox.Show("Please enter a value for X axis.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Y_Input.Text))
            {
                MessageBox.Show("Please enter a value for Y axis.");
                return;
            }
            if (Height_Input.Visible && string.IsNullOrWhiteSpace(Height_Input.Text))
            {
                MessageBox.Show("Please enter a value for width.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Width_Input.Text))
            {
                MessageBox.Show("Please enter a value for height.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Sides_Input.Text))
            {
                MessageBox.Show("Please enter a value for sides.");
                return;
            }
            X = int.Parse(X_Input.Text);
            Y = int.Parse(Y_Input.Text);
            Width_Value = int.Parse(Width_Input.Text);
            if (Height_Input.Visible)
                Height_Value = int.Parse(Height_Input.Text);
            else
                Height_Value = Width_Value;
            Sides_Value = int.Parse(Sides_Input.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CalculationForm_Load(object sender, EventArgs e)
        {
        }

        public void Toggle_Height_Input_Visibility(bool visible)
        {
            Height_Input.Visible = visible;
            Height_Label.Visible = visible;
        }
    }
}
