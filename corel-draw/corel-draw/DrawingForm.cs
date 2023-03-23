using corel_draw.Figures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace corel_draw
{
    public partial class DrawingForm : Form
    {
        private List<Figure> drawnFigures = new List<Figure>();
        private Figure currentFigure;
        private bool isDragging = false;
        private Point offset;
        public DrawingForm()
        {
            InitializeComponent();
        }

        private void CreateFigure(bool showHeightField,Type figureType)
        {
            //change logic here for polygon
            CalculationForm calculationForm = new CalculationForm();
            calculationForm.Toggle_Height_Input_Visibility(showHeightField);
            DialogResult result = calculationForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (figureType == typeof(Figures.Square))
                {
                    Figure figure = (Figure)Activator.CreateInstance(figureType, new object[] { calculationForm.X, calculationForm.Y, calculationForm.Height_Value, calculationForm.Height_Value });
                    drawnFigures.Add(figure);
                    pictureBox1.Invalidate();
                }
                else
                {
                    Figure figure = (Figure)Activator.CreateInstance(figureType, new object[] { calculationForm.X, calculationForm.Y, calculationForm.Width_Value, calculationForm.Height_Value });
                    drawnFigures.Add(figure);
                    pictureBox1.Invalidate();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateFigure(true,typeof(Figures.Rectangle));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateFigure(true, typeof(Figures.Circle));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //still working on the polygon type figures
            PolygonForm polygonForm = new PolygonForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateFigure(false, typeof(Figures.Square));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //still working on the polygon type figures
            TriangleForm triangleForm = new TriangleForm();
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;

            var figureTypes = typeof(Figure).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Figure))).ToArray();
            var buttonWidth = (Width - 150) / figureTypes.Length;
            for (int i = 0; i < figureTypes.Length; i++)
            {
                var figureType = figureTypes[i];
                int index = i;
                var button = new Button
                {
                    Text = figureType.Name,
                    Tag = figureType,
                    Height = 75,
                    Width = buttonWidth,
                    Left = i * buttonWidth + 75,
                    Top = Height - 150
                };

                button.Click += (object sender1, EventArgs e1) =>
                {
                    CreateFigure(true, figureTypes[index]);
                };
                Controls.Add(button);
                button.Show();
                button.BringToFront();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Figure figure in drawnFigures)
            {
                if (figure.Contains(e.Location))
                {
                    currentFigure = figure;
                    isDragging = true;
                    offset = new Point(e.X - figure.Location.X, e.Y - figure.Location.Y);
                    break;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                currentFigure.Location = new Point(e.X - offset.X, e.Y - offset.Y);
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Figure figure in drawnFigures)
            {
                figure.Draw(e.Graphics);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
