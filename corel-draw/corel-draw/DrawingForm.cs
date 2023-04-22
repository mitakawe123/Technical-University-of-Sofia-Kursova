using corel_draw.Components;
using corel_draw.FactoryComponents;
using corel_draw.Figures;
using CorelLibary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        private readonly List<Figure> drawnFigures;
        private readonly IReadOnlyList<FigureFactory> _figureFactories;
        private FigureFactory _figureFactory;
        private readonly AdditionalInfo additionalInfo;
        private readonly CommandManager commandManager;
        private Action<Figure> figureFinishedHandler;

        private Figure currentFigure;
        private Point? lastPoint = null;
        private Point? initialPosition = null;

        private bool isDragging = false;
        private bool isFilling = false;

        private readonly string path = "../../JsonFiles/DataFigures.json";

        enum SpecialTags
        {
            BiggestFigure,
            MostSidesForPolygon,
            FirstFigure,
            LastFigure,
            EditedFigure
        }

        public DrawingForm(IReadOnlyList<FigureFactory> figureFactories)
        {
            InitializeComponent(); 
            commandManager = new CommandManager();
            additionalInfo = new AdditionalInfo();
            drawnFigures = new List<Figure>();
            _figureFactories = figureFactories;
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {
            Type[] figureTypes = typeof(Figure).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Figure))).ToArray();
            int buttonWidth = (Width - 150) / figureTypes.Length;
            for (int i = 0; i < _figureFactories.Count; i++)
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
                    _figureFactory = _figureFactories[index];
                    _figureFactory.BeginCreateFigure();
                    isFilling = false;
                };
                Controls.Add(button);
                figureFinishedHandler = (figure) =>
                {
                    ICommand addCommand = new AddCommand(figure, drawnFigures);
                    commandManager.AddCommand(addCommand);
                    _figureFactory = null;
                    actionList.Items.Add($"Added {figure.GetType().Name} with area of {figure.CalcArea():F2}");
                    DrawingBox.Invalidate();
                }; 
                _figureFactories[index].Finished += figureFinishedHandler;
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
            Figure oldState = currentFigure;
            int matchingIndex = -1;
            Type[] figureTypes = typeof(Figure).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Figure))).ToArray();

            for (int i = 0; i < figureTypes.Length; i++)
            {
                if (currentFigure.GetType() == figureTypes[i])
                {
                    matchingIndex = i;
                    break;
                }
            }
            _figureFactory = _figureFactories[matchingIndex];
            _figureFactory.BeginCreateFigure();

            _figureFactories[matchingIndex].Finished -= figureFinishedHandler;
            _figureFactories[matchingIndex].Finished += (figure) =>
            {
                ICommand command = new EditCommand(oldState, figure);
                commandManager.AddCommand(command);
                _figureFactory = null;
                currentFigure = figure;
                actionList.Items.Add($"Edit {oldState.GetType().Name} with new area of {figure.CalcArea():F2}");
                DrawingBox.Invalidate();
            };
        }

        private void AdditionalInfoMenuItem_Click(object sender, EventArgs e)
        {
            Figure largestFigure = drawnFigures.OrderByDescending(f => f.CalcArea()).First();
            Figure smallestFigure = drawnFigures.OrderBy(f => f.CalcArea()).First();
            
            additionalInfo.ShowDialog();
        }

        private void DrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (_figureFactory != null)
            {
                _figureFactory.MouseDown(e);
                DrawingBox.Invalidate();
                return;
            }
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
                        contextMenuStrip.Items.Add("Info").Click += AdditionalInfoMenuItem_Click;
                        contextMenuStrip.Items[2].Tag = currentFigure;

                        contextMenuStrip.Show(DrawingBox, e.Location);
                    }
                    break;
                }
            }
        }
        [DebuggerStepThrough]
        private void DrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(e.X - lastPoint.Value.X, e.Y - lastPoint.Value.Y);
                lastPoint = e.Location;
                currentFigure.Move(delta);
                DrawingBox.Invalidate();
            }
            else if(!isDragging && _figureFactory != null)
            {
                _figureFactory.MouseMove(e);
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
            else if (!isDragging && _figureFactory != null) 
            {
                _figureFactory.MouseUp(e);
                DrawingBox.Invalidate();
            }
        }

        private void DrawingBox_Paint(object sender, PaintEventArgs e)
        {
            _figureFactory?.Draw(e.Graphics);
            foreach (Figure figure in drawnFigures)
            {
                figure.Draw(e.Graphics);
                if(isFilling)
                {
                    figure.Fill(e.Graphics);
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
