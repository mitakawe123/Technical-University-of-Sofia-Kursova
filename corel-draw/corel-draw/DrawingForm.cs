using corel_draw.Figures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private Stack<Figure> undoFigures = new Stack<Figure>();
        private Stack<Figure> redoFigures = new Stack<Figure>();

        private Figure currentFigure;
        private bool isDragging = false;
        private Point offset;
        public DrawingForm()
        {
            InitializeComponent();
        }

        private void CreateFigure(Type figureType, bool PolygonType)
        {
            if (PolygonType)
            {
                PolygonSides polygonSides = new PolygonSides();
                DialogResult sidesResult = polygonSides.ShowDialog();
                if (sidesResult == DialogResult.OK)
                {
                    PolygonTypeForm polygonTypeForm = new PolygonTypeForm(polygonSides.Sides);
                    DialogResult polygonTypeResult = polygonTypeForm.ShowDialog();
                    if (polygonTypeResult == DialogResult.OK)
                    {
                        Figure figure = (Figure)Activator.CreateInstance(figureType, new object[]
                        {
                            polygonTypeForm.PolygonPoints
                        });

                        drawnFigures.Add(figure);
                        pictureBox1.Invalidate();
                    }
                }
            }
            else
            {
                CalculationForm calculationForm = new CalculationForm(figureType);
                DialogResult result = calculationForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Figure figure = (Figure)Activator.CreateInstance(figureType, new object[]
                    {
                        calculationForm.X,
                        calculationForm.Y,
                        calculationForm.Width_Value,
                        calculationForm.Height_Value,
                    });

                    drawnFigures.Add(figure);
                    pictureBox1.Invalidate();
                }
            }
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
                bool isPolygonType = figureType == typeof(Polygon) ? true : false;
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
                    CreateFigure(figureTypes[index], isPolygonType);
                };
                Controls.Add(button);
                button.Show();
                button.BringToFront();
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFigure != null)
            {
                drawnFigures.Remove(currentFigure);
                undoFigures.Push(currentFigure);
                actionList.Items.Add($"Delete {currentFigure.GetType().Name}");
                currentFigure = null;
                pictureBox1.Invalidate();
            }
        }

        private void ColorMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (currentFigure != null)
                {
                    currentFigure.Color = colorDialog.Color;
                    actionList.Items.Add($"Change Color to {colorDialog.Color.Name}");
                    pictureBox1.Invalidate();
                }
            }
        }        

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalculationForm calculationForm = new CalculationForm(currentFigure.GetType());
            DialogResult result = calculationForm.ShowDialog();

            if (currentFigure.GetType() == typeof(Polygon))
            {
                //TODO: check for polygon and open the corrent form
            } 

            if (result == DialogResult.OK)
            {
                if (currentFigure.GetType() == typeof(Polygon))
                {
                    //TODO: set the correct location for polygon when edit
                }
                else
                {
                    currentFigure.Location = new Point(calculationForm.X, calculationForm.Y);
                    currentFigure.Width = calculationForm.Width_Value;
                    currentFigure.Height = calculationForm.Height_Value;
                    actionList.Items.Add($"Edit {currentFigure.GetType().Name}");
                    pictureBox1.Invalidate();
                }
            }
        }
        private Point? lastPoint = null;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Figure figure in drawnFigures)
            {
                if (figure is Polygon polygon && polygon.Contains(e.Location))
                {
                    currentFigure = figure;
                    isDragging = true;
                    lastPoint = e.Location;
                    break;
                }
                else if (figure.Contains(e.Location))
                {
                    currentFigure = figure;
                    isDragging = true;
                    offset = new Point(e.X - figure.Location.X, e.Y - figure.Location.Y);
                    break;
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                foreach (Figure figure in drawnFigures)
                {
                    if (figure.Contains(e.Location))
                    {
                        currentFigure = figure;

                        ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

                        // delete 
                        ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Delete");
                        deleteMenuItem.Click += new EventHandler(DeleteMenuItem_Click);
                        contextMenuStrip.Items.Add(deleteMenuItem);

                        // color
                        ToolStripMenuItem colorMenuItem = new ToolStripMenuItem("Change Color");
                        colorMenuItem.Click += new EventHandler(ColorMenuItem_Click);
                        contextMenuStrip.Items.Add(colorMenuItem);

                        //edit
                        ToolStripMenuItem editToolStripMenuItem = new ToolStripMenuItem("Edit");
                        editToolStripMenuItem.Tag = currentFigure;
                        editToolStripMenuItem.Click += new EventHandler(EditToolStripMenuItem_Click);
                        contextMenuStrip.Items.Add(editToolStripMenuItem);

                        contextMenuStrip.Show(pictureBox1, e.Location);
                        break;
                    }
                }
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && currentFigure is Polygon polygon)
            {
                int dx = e.X - lastPoint.Value.X;
                int dy = e.Y - lastPoint.Value.Y;
                lastPoint = e.Location;
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Point(polygon.Points[i].X + dx, polygon.Points[i].Y + dy);
                }
                pictureBox1.Invalidate();
            }
            else if (isDragging)
            {
                currentFigure.Location = new Point(e.X - offset.X, e.Y - offset.Y);
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false; 
            lastPoint = null;
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Figure figure in drawnFigures)
            {
                figure.Draw(e.Graphics);
            }
        }

        private void Redo_Btn_Click(object sender, EventArgs e)
        {
            if (redoFigures.Count > 0)
            {
                Figure lastRedoFigure = redoFigures.Pop();
                actionList.Items.Add($"Redo {lastRedoFigure.GetType().Name}");
                drawnFigures.Remove(lastRedoFigure);
                undoFigures.Push(lastRedoFigure);
                pictureBox1.Invalidate();
            }
        }

        private void Undo_Btn_Click(object sender, EventArgs e)
        {
            if (undoFigures.Count > 0)
            {
                Figure lastDeletedFigure = undoFigures.Pop();
                actionList.Items.Add($"Undo {lastDeletedFigure.GetType().Name}");
                drawnFigures.Add(lastDeletedFigure);
                redoFigures.Push(lastDeletedFigure);
                pictureBox1.Invalidate();
            }
        }
    }
}
