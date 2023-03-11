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
    public partial class DrawingForm : Form
    {
        public float xAxisVal;
        public float yAxisVal;
        public float widthVal;
        public float heightVal;
        public DrawingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Figures.Rectangle rectangle = new Figures.Rectangle();
            Graphics graphics = this.CreateGraphics();
            rectangle.DrawFigure(new PaintEventArgs(graphics, this.ClientRectangle), xAxisVal, yAxisVal, widthVal, heightVal);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PaintEventArgs paintEventArgs = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
            Figures.Circle circle = new Figures.Circle();
            circle.DrawFigure(paintEventArgs, xAxisVal, yAxisVal, widthVal, heightVal);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PolygonForm polygonForm = new PolygonForm();
            polygonForm.Show();
            /*this.Hide();*/
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Figures.Square square = new Figures.Square();
            Graphics graphics = this.CreateGraphics();
            square.DrawFigure(new PaintEventArgs(graphics, this.ClientRectangle),xAxisVal, yAxisVal, widthVal, heightVal);
            //here i need to make so that the width or height input in the main form is hidden
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Figures.Triagnle triangle = new Figures.Triagnle();
            Graphics graphics = this.CreateGraphics();
            Brush brush = new SolidBrush(Color.Black);
            PointF point1 = new PointF(50, 50);
            PointF point2 = new PointF(100, 150);
            PointF point3 = new PointF(150, 50);
            triangle.DrawTriangle(graphics, brush, point1, point2, point3);
        }
    }
}
