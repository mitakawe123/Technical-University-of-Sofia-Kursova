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
        public CalculationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //passing the values from calculation form to the main form
            string xAxis = textBox1.Text;
            string yAxis = textBox2.Text;
            string width= textBox3.Text;
            string height = textBox4.Text;
            DrawingForm drawingForm = new DrawingForm();
            drawingForm.xAxisVal = float.Parse(xAxis);
            drawingForm.yAxisVal = float.Parse(yAxis);
            drawingForm.widthVal = float.Parse(width);
            drawingForm.heightVal = float.Parse(height);
        }
    }
}
