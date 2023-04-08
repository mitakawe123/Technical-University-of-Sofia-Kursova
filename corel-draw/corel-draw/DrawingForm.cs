using corel_draw.Components;
using corel_draw.Figures;
using corel_draw.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace corel_draw
{
    public partial class DrawingForm : Form
    {
        private readonly List<Figure> drawnFigures = new List<Figure>();
        private readonly List<Point> clickedPoints = new List<Point>();
        private readonly CommandManager commandManager;

        private readonly Bitmap bitmap;
        private Figure currentFigure;
        private bool isDragging = false;
        private Point? lastPoint = null;

        private bool _isEditing = false;

        private PolygonSides polygonSides = new PolygonSides();
        private readonly string path = "../../JsonFiles/DataFigures.json";

        public DrawingForm()
        {
            InitializeComponent(); 
            bitmap = new Bitmap(DrawingBox.Width, DrawingBox.Height);
            commandManager = new CommandManager(actionList);
        }

        private void CreateFigure(Type figureType, bool PolygonType)
        {
            if (PolygonType)
            {
                polygonSides.ShowDialog();
                _isEditing = false;
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

                ICommand addCommand = new AddCommand(figure, drawnFigures);
                commandManager.AddCommand(addCommand);

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
                ICommand removeCommand = new DeleteCommand(currentFigure, drawnFigures);
                commandManager.AddCommand(removeCommand);
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
                    Color oldColor = currentFigure.Color;
                    Color newColor = colorDialog.Color;

                    ICommand colorCommand = new ColorCommand(currentFigure, oldColor, newColor);
                    commandManager.AddCommand(colorCommand);

                    currentFigure.Color = newColor;
                    actionList.Items.Add($"Change Color to {newColor.Name}");
                    DrawingBox.Invalidate();
                }
            }
        }
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFigure is Polygon)
            {
                polygonSides = new PolygonSides();
                DialogResult dialogResult =  polygonSides.ShowDialog();
                if (dialogResult == DialogResult.OK) _isEditing = true;
            }
            else
            {
                Figure oldState = currentFigure;
                CalculationForm calculationForm = new CalculationForm(currentFigure.GetType());
                DialogResult result = calculationForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Figure newState = new Figure
                    {
                        Location = new Point(calculationForm.X, calculationForm.Y),
                        Width = calculationForm.Width_Value,
                        Height = calculationForm.Height_Value
                    };

                    ICommand command = new EditCommand(oldState, newState);
                    commandManager.AddCommand(command);

                    currentFigure = newState;
                    actionList.Items.Add($"Edit {currentFigure.GetType().Name} with new area of: {currentFigure.CalcArea():F2}");
                    DrawingBox.Invalidate();
                }
            }
        }

        private Point? initialPosition = null;
        private void DrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!polygonSides.IsDrawing)
            {
                foreach (Figure figure in drawnFigures)
                {
                    if (figure.Contains(e.Location))
                    {
                        currentFigure = figure;
                        isDragging = true;
                        lastPoint = e.Location;
                        initialPosition = figure.Location;
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
            }
            else if (polygonSides.IsDrawing)
            {
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
                    Figure oldState = currentFigure;
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        Pen pen = new Pen(Color.Black, 2f);
                        g.DrawLine(pen, clickedPoints[clickedPoints.Count - 2], clickedPoints[clickedPoints.Count - 1]);
                    }

                    Figure newState = new Figure();
                    currentFigure = new Polygon(clickedPoints.ToList());
                    currentFigure.Name = currentFigure.GetType().Name;
                    
                    newState = currentFigure;

                    if (!_isEditing)
                    {
                        ICommand addCommand = new AddCommand(currentFigure, drawnFigures);
                        commandManager.AddCommand(addCommand);
                        actionList.Items.Add($"The area of the {currentFigure.GetType().Name} is: {currentFigure.CalcArea():F2}");
                    }
                    else if(_isEditing)
                    {
                        ICommand command = new EditCommand(oldState,newState);
                        commandManager.AddCommand(command);
                        currentFigure = newState;
                        actionList.Items.Add($"Edit {currentFigure.GetType().Name} with new area of: {currentFigure.CalcArea():F2}");
                    }

                    clickedPoints.Clear();
                    polygonSides.IsDrawing = false;
                }
            }
        }
        

        private void DrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(e.X - lastPoint.Value.X, e.Y - lastPoint.Value.Y);
                lastPoint = e.Location;

                ICommand moveCommand = new MoveCommand(currentFigure, delta,initialPosition.Value);
                commandManager.AddCommand(moveCommand);
                
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
                if (polygonSides.IsDrawing)
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
            if (commandManager.CanRedo())
            {
                commandManager.Redo();
                DrawingBox.Invalidate();
            }
        }

        private void Undo_Btn_Click(object sender, EventArgs e)
        {
            if (commandManager.CanUndo())
            {
                commandManager.Undo();
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
