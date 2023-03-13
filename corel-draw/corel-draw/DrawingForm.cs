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
        CalculationForm calcForm = new CalculationForm();
        private List<Figure> figures = new List<Figure>();
        private bool isDragging = false;
        private Point lastMouseLocation;

        public DrawingForm()
        {
            InitializeComponent();
        }

        private void RedrawForm()
        {
            if(figures.Count > 0)
            {
                figures.RemoveAt(0);
                this.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RedrawForm();
            calcForm.isSquare = false;
            calcForm.ShowTextBoxForSquare();
            DialogResult result = calcForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                calcForm.nameOfFigure = "Put your measurements for Rectangle";
                Figures.Rectangle rectangle = new Figures.Rectangle();
                Graphics graphics = this.CreateGraphics();
                rectangle.DrawFigure(new PaintEventArgs(graphics, this.ClientRectangle), calcForm.xAxisVal, calcForm.yAxisVal, calcForm.widthVal, calcForm.heightVal);
                figures.Add(rectangle);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            RedrawForm();
            DialogResult result = calcForm.ShowDialog(); 
            calcForm.isSquare = false;
            calcForm.ShowTextBoxForSquare();
            if (result == DialogResult.OK)
            {
                calcForm.nameOfFigure = "Put your measurements for circle";
                PaintEventArgs paintEventArgs = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
                Figures.Circle circle = new Figures.Circle();
                circle.DrawFigure(paintEventArgs, calcForm.xAxisVal, calcForm.yAxisVal, calcForm.widthVal, calcForm.heightVal);
                figures.Add(circle);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RedrawForm();
            PolygonForm polygonForm = new PolygonForm();
            polygonForm.Show();
            /*this.Hide();*/
            /*figures.Add();*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RedrawForm();

            calcForm.isSquare = true;
            calcForm.HideTextBoxForSquare();
            DialogResult result = calcForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                calcForm.nameOfFigure = "Put your measurements for square";
                Figures.Square square = new Figures.Square();
                Graphics graphics = this.CreateGraphics();
                square.DrawFigure(new PaintEventArgs(graphics, this.ClientRectangle), calcForm.xAxisVal, calcForm.yAxisVal, calcForm.widthVal, calcForm.heightVal);
                figures.Add(square);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RedrawForm();
            calcForm.Show();
            calcForm.isSquare = false;
            calcForm.nameOfFigure = "Put your measurements for Triangle";
            Figures.Triagnle triangle = new Figures.Triagnle();
            Graphics graphics = this.CreateGraphics();
            Brush brush = new SolidBrush(Color.Black);
            PointF point1 = new PointF(50, 50);
            PointF point2 = new PointF(100, 150);
            PointF point3 = new PointF(150, 50);
            triangle.DrawTriangle(graphics, brush, point1, point2, point3);
            figures.Add(triangle);
        }

        private void DrawingForm_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void DrawingForm_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void DrawingForm_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        //trying to move each figure with the key strokes   
        private void DrawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyData == Keys.Up)
            {
                figures.Last().Y -= 10;
            }
            if (e.KeyData == Keys.Down)
            {
                figures.Last().Y += 10;
            }
            if (e.KeyData == Keys.Left)
            {
                figures.Last().X -= 10;
            }
            if (e.KeyData == Keys.Right)
            {   
                figures.Last().X += 10;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }
}
