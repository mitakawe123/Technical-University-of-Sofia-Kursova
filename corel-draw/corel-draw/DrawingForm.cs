using corel_draw.Components;
using corel_draw.Figures;
using corel_draw.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
        private readonly List<Figure> drawnFigures = new List<Figure>();
        private readonly Stack<Figure> undoFigures = new Stack<Figure>();
        private readonly Stack<Figure> redoFigures = new Stack<Figure>();
        private readonly List<Point> clickedPoints = new List<Point>();
        
        private readonly Bitmap bitmap;
        private Figure currentFigure;
        private bool isDragging = false;
        private Point offset;
        private Point? lastPoint = null;

        private PolygonSides polygonSides = new PolygonSides();
        private readonly string path = "../../JsonFiles/DataFigures.json";

        public DrawingForm()
        {
            InitializeComponent(); 
            bitmap = new Bitmap(DrawingBox.Width, DrawingBox.Height);
        }

        private void CreateFigure(Type figureType, bool PolygonType)
        {
            if (PolygonType)
            {
                polygonSides.ShowDialog();
                return;
            }
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
                actionList.Items.Add($"The area of the {figure.GetType().Name} is: {figure.CalcArea():F2}");
                figure.Name = figure.GetType().Name;
                drawnFigures.Add(figure);
                DrawingBox.Invalidate();
            }
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {
            DrawingBox.BackColor = Color.White;
            DrawingBox.SizeMode = PictureBoxSizeMode.StretchImage;

            var figureTypes = typeof(Figure).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Figure))).ToArray();
            var buttonWidth = (Width - 150) / figureTypes.Length;
            for (int i = 0; i < figureTypes.Length; i++)
            {
                var figureType = figureTypes[i];
                bool isPolygonType = figureType == typeof(Polygon);
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
                DrawingBox.Invalidate();
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
                    DrawingBox.Invalidate();
                }
            }
        }        

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFigure is Polygon)
            {
                polygonSides = new PolygonSides();
                DialogResult result = polygonSides.ShowDialog();
                if(result == DialogResult.OK)
                {
                    drawnFigures.Remove(currentFigure);
                }
            } else
            {
                CalculationForm calculationForm = new CalculationForm(currentFigure.GetType());
                DialogResult result = calculationForm.ShowDialog();
                if(result == DialogResult.OK)
                {
                    currentFigure.Location = new Point(calculationForm.X, calculationForm.Y);
                    currentFigure.Width = calculationForm.Width_Value;
                    currentFigure.Height = calculationForm.Height_Value;
                    actionList.Items.Add($"The area of the {currentFigure.GetType().Name} is: {currentFigure.CalcArea():F2}");
                    DrawingBox.Invalidate();
                }
            }
        }

        private void DrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!polygonSides.isDrawing)
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

                            // edit
                            ToolStripMenuItem editToolStripMenuItem = new ToolStripMenuItem("Edit")
                            {
                                Tag = currentFigure
                            };
                            editToolStripMenuItem.Click += new EventHandler(EditToolStripMenuItem_Click);
                            contextMenuStrip.Items.Add(editToolStripMenuItem);

                            contextMenuStrip.Show(DrawingBox, e.Location);
                            break;
                        }
                    }
                }
                return;
            }

            clickedPoints.Add(e.Location);

            if (clickedPoints.Count > 1)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    Pen pen = new Pen(Color.Black, 2f);
                    g.DrawLine(pen, clickedPoints[clickedPoints.Count - 2], clickedPoints[clickedPoints.Count - 1]);
                }
            }

            DrawingBox.Invalidate();

            if (clickedPoints.Count == polygonSides.Sides)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    Pen pen = new Pen(Color.Black, 2f);
                    g.DrawLine(pen, clickedPoints[clickedPoints.Count - 2], clickedPoints[clickedPoints.Count - 1]);
                }

                currentFigure = new Polygon(clickedPoints.ToList());
                drawnFigures.Add(currentFigure);
                currentFigure.Name = currentFigure.GetType().Name;

                actionList.Items.Add($"The area of the {currentFigure.GetType().Name} is: {currentFigure.CalcArea():F2}");

                clickedPoints.Clear();
                polygonSides.isDrawing = false;
            }
        }
        private void DrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && currentFigure is Polygon polygon)
            {
                Point delta = new Point(e.X - lastPoint.Value.X, e.Y - lastPoint.Value.Y);
                lastPoint = e.Location;
                polygon.Move(delta);
                DrawingBox.Invalidate();
            }
            else if (isDragging)
            {
                currentFigure.Location = new Point(e.X - offset.X, e.Y - offset.Y);
                DrawingBox.Invalidate();
            }
        }

        private void DrawingBox_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false; 
            lastPoint = null;
        }

        private void DrawingBox_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Black, 2f))
            {
                if (polygonSides.isDrawing)
                {
                    if (clickedPoints.Count > 1)
                    {
                        e.Graphics.DrawLines(pen, clickedPoints.ToArray());
                        e.Graphics.DrawLine(pen, clickedPoints[clickedPoints.Count - 1], clickedPoints[0]);
                    }

                    using (GraphicsPath path = new GraphicsPath())
                    {
                        foreach (Point p in clickedPoints)
                        {
                            path.AddEllipse(p.X - 3, p.Y - 3, 6, 6);
                        }
                        e.Graphics.FillPath(Brushes.Black, path);
                    }
                }
                else
                {
                    foreach (Figure figure in drawnFigures)
                    {
                        figure.Draw(e.Graphics);
                    }
                }
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
                DrawingBox.Invalidate();
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
                DrawingBox.Invalidate();
            }
        }

        private void SaveToFile_Click(object sender, EventArgs e)
        {
            try
            {
                var drawingData = new DrawingData { DrawnFigures = drawnFigures };
                string json = JsonConvert.SerializeObject(drawingData);
                File.WriteAllText(path, json);
                MessageBox.Show("File saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}");
            }
        }

        private void LoadFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                string json = File.ReadAllText(path);
                var drawingData = JsonConvert.DeserializeObject<DrawingData>(json);

                foreach (var figureData in drawingData.DrawnFigures)
                {
                    Figure figure;
                    switch (figureData.Name)
                    {
                        case "Circle":
                            figure = new Circle(figureData.Location.X, figureData.Location.Y, figureData.Width, figureData.Height);
                            break;
                        case "Rectangle":
                            figure = new Figures.Rectangle(figureData.Location.X, figureData.Location.Y, figureData.Width, figureData.Height);
                            break;
                        case "Square":
                            MessageBox.Show(figureData.GetType().Name);
                            figure = new Square(figureData.Location.X, figureData.Location.Y, figureData.Width, figureData.Width);
                            break;
                        case "Polygon":
                            figure = new Polygon(figureData.Points);
                            break;
                        default:
                            throw new ArgumentException($"Invalid figure name: {figureData.Name}");
                    }
                    figure.Color = figureData.Color;
                    drawnFigures.Add(figure);
                }

                DrawingBox.Invalidate();
                MessageBox.Show("File loaded successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}");
            }
        }
    }
}
