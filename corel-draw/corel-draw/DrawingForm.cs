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
    public partial class DrawingForm : Form
    {
        public DrawingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Figures.Rectangle rectangle = new Figures.Rectangle();
            Graphics graphics = this.CreateGraphics();
            rectangle.DrawFigure(new PaintEventArgs(graphics, this.ClientRectangle), 150, 150, 100, 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PaintEventArgs paintEventArgs = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
            Figures.Circle circle = new Figures.Circle();
            circle.DrawFigure(paintEventArgs, 100,100,100,100);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PolygonForm polygon = new PolygonForm();
            polygon.Show();
            /*this.Hide();*/
        }
    }
}
