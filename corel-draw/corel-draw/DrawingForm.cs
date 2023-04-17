using corel_draw.Components;
using corel_draw.Figures;
using CorelLibary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private Point? lastPoint = null;
        private Point? initialPosition = null;

        private bool _isEditing = false;
        private bool isDragging = false;
        private bool isFilling = false;

        private PolygonSides polygonSides = new PolygonSides();
        private readonly string path = "../../JsonFiles/DataFigures.json";

        public DrawingForm()
        {
            InitializeComponent(); 
            bitmap = new Bitmap(DrawingBox.Width, DrawingBox.Height);
            commandManager = new CommandManager();
            //Undo_Btn.Enabled = Redo_Btn.Enabled = false;
        }

        private void CreateFigure(Type figureType)
        {
            ConstructorInfo constructor = figureType.GetConstructor(new Type[] { typeof(List<Point>) });
            if (constructor != null)
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

                figure.Name = figure.GetType().Name;

                ICommand addCommand = new AddCommand(figure, drawnFigures);
                commandManager.AddCommand(addCommand);
                actionList.Items.Add($"Added {figure.GetType().Name} with area {figure.CalcArea():F2}");
                DrawingBox.Invalidate();
            }
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {
            Type[] figureTypes = typeof(Figure).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Figure))).ToArray();
            int buttonWidth = (Width - 150) / figureTypes.Length;
            for (int i = 0; i < figureTypes.Length; i++)
            {
                Type figureType = figureTypes[i];
                int index = i;
                Button button = new Button
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
                    CreateFigure(figureTypes[index]);
                    isFilling = false;
                };
                Controls.Add(button);
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

                    actionList.Items.Add($"Change {currentFigure.GetType().Name} Color with {currentFigure.Color.Name}");
                    currentFigure.Color = newColor;
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
                    Type figureType = currentFigure.GetType();
                    Figure newState = (Figure)Activator.CreateInstance(
                            figureType, 
                            calculationForm.X, 
                            calculationForm.Y, 
                            calculationForm.Width_Value, 
                            calculationForm.Height_Value
                        );

                    ICommand command = new EditCommand(oldState, newState);
                    commandManager.AddCommand(command);

                    actionList.Items.Add($"Edit {oldState.GetType().Name} with new area of {newState.CalcArea():F2}");
                    currentFigure = newState;
                    DrawingBox.Invalidate();
                }
            }
        }

        private void DrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (polygonSides.IsDrawing)
            {
                clickedPoints.Add(e.Location);

                if (clickedPoints.Count > 1)
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawLine(new Pen(Color.Black, 2f), clickedPoints[clickedPoints.Count - 2], clickedPoints[clickedPoints.Count - 1]);
                    }
                }

                DrawingBox.Invalidate();

                if (clickedPoints.Count == polygonSides.Sides)
                {
                    Figure oldState = currentFigure;
                    currentFigure = new Polygon(clickedPoints.ToList());
                    currentFigure.Name = currentFigure.GetType().Name;

                    if (!_isEditing)
                    {
                        commandManager.AddCommand(new AddCommand(currentFigure, drawnFigures));
                        actionList.Items.Add($"Added {currentFigure.GetType().Name} with area {currentFigure.CalcArea():F2}");
                    }
                    else
                    {
                        commandManager.AddCommand(new EditCommand(oldState, currentFigure));
                        actionList.Items.Add($"Edit {oldState.GetType().Name} with new area of {currentFigure.CalcArea():F2}");
                    }
                    clickedPoints.Clear();
                    polygonSides.IsDrawing = false;
                }
            }
            else
            {
                foreach (Figure figure in drawnFigures)
                {
                    if (figure.Contains(e.Location))
                    {
                        currentFigure = figure;
                        if (e.Button == MouseButtons.Left)
                        {
                            isDragging = true;
                            lastPoint = e.Location;
                            initialPosition = figure.Location;
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

                            contextMenuStrip.Items.Add("Delete").Click += DeleteMenuItem_Click;
                            contextMenuStrip.Items.Add("Change Border Color").Click += ColorMenuItem_Click;
                            contextMenuStrip.Items.Add("Fill Figure").Click += FillMenuItem_Click;
                            contextMenuStrip.Items.Add("Edit").Click += EditToolStripMenuItem_Click;
                            contextMenuStrip.Items[2].Tag = currentFigure;

                            contextMenuStrip.Show(DrawingBox, e.Location);
                        }
                        break;
                    }
                }
            }
        }

        private void DrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(e.X - lastPoint.Value.X, e.Y - lastPoint.Value.Y);
                lastPoint = e.Location;
                currentFigure.Move(delta);
                DrawingBox.Invalidate();
            }
        }

        private void DrawingBox_MouseUp(object sender, MouseEventArgs e)
        {
            if(isDragging)
            {
                Point delta = new Point(e.X - lastPoint.Value.X, e.Y - lastPoint.Value.Y);
                ICommand moveCommand = new MoveCommand(currentFigure, delta, initialPosition.Value);
                commandManager.AddCommand(moveCommand);
                actionList.Items.Add($"Move {currentFigure.GetType().Name}");
                isDragging = false;
                lastPoint = null;
            }
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
                        if(isFilling)
                        {
                            figure.Fill(e.Graphics);
                        }
                    }
                }
            }
        }

        private void FillMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (currentFigure != null)
                {
                    Color oldFilling = currentFigure.FillColor;
                    Color newFilling = colorDialog.Color;
                    isFilling = true;

                    ICommand command = new FillCommand(currentFigure, oldFilling, newFilling);
                    commandManager.AddCommand(command);

                    actionList.Items.Add($"Change {currentFigure.GetType().Name} Fill Color with {currentFigure.FillColor.Name}");
                    currentFigure.FillColor = newFilling;
                    DrawingBox.Invalidate();
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
            //else Redo_Btn.Enabled = false;
        }

        private void Undo_Btn_Click(object sender, EventArgs e)
        {
            if (commandManager.CanUndo())
            {
                commandManager.Undo();
                DrawingBox.Invalidate();
            }
            //else Undo_Btn.Enabled = false;
        }

        private void SaveToFile_Click(object sender, EventArgs e)
        {
            try
            {
                DrawingData drawingData = new DrawingData { DrawnFigures = drawnFigures.ToList() };
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                };
                string json = JsonConvert.SerializeObject(drawingData, Formatting.Indented, settings);
                File.WriteAllText(path, json);
                MessageBox.Show("File saved successfully.");
                actionList.Items.Add("Save figures to file");
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
                DrawingData drawingData = JsonConvert.DeserializeObject<DrawingData>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

                List<Figure> loadedFigures = new List<Figure>();
                loadedFigures.AddRange(drawingData.DrawnFigures);

                ICommand loadCommand = new LoadCommand(drawnFigures, loadedFigures);
                commandManager.AddCommand(loadCommand);

                DrawingBox.Invalidate();
                MessageBox.Show("File loaded successfully.");
                actionList.Items.Add("Load figures from file");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}");
            }
        }
    }
}
