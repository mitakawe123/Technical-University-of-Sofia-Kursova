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
    public partial class PolygonForm : Form
    {
        DrawingForm drawingForm = new DrawingForm();
        
        public PolygonForm()
        {
            InitializeComponent();
        }

        private void PolygonForm_Load(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Figures.Polygon polygon = new Figures.Polygon();
            PaintEventArgs paintEventArgs = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
            polygon.DrawPolygon(paintEventArgs, float.Parse(textBox1.Text), float.Parse(textBox2.Text), float.Parse(textBox3.Text), float.Parse(textBox4.Text), int.Parse(textBox5.Text));
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
