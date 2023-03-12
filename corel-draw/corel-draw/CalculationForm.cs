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
        public string nameOfFigure 
        { 
            get { return label1.Text; } 
            set { label1.Text = value; } 
        }
        public float xAxisVal { get; set; }
        public float yAxisVal { get; set; }
        public float widthVal { get; set; }
        public float heightVal { get; set; }
        public CalculationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            xAxisVal = float.Parse(textBox1.Text);
            yAxisVal = float.Parse(textBox2.Text);
            widthVal = float.Parse(textBox3.Text);
            heightVal = float.Parse(textBox4.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CalculationForm_Load(object sender, EventArgs e)
        {
        }

        public void HideTextBoxForSquare()
        {
            textBox4.Hide();
            label5.Hide();
        }
    }
}
