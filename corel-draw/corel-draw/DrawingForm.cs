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
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace corel_draw
{
    public partial class DrawingForm : Form
    {
        private const string FACTORY_SUFFIX = "Factory";
        private const string PATH = "../../JsonFiles/DataFigures.json";
        private const int WIDTH = 75;
        private const int HEIGHT = 150;

        private static readonly Type[] FigureTypes = typeof(FigureFactory).Assembly
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(FigureFactory)))
                .ToArray();

        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        private readonly Pen _dashedPen = new Pen(Color.Cyan, 5) { DashStyle = DashStyle.Dot };
        private readonly IReadOnlyList<FigureFactory> _figureFactories;
        private readonly List<Figure> _drawnFigures;
        private readonly CommandManager _commandManager;
        
        private FigureFactory _figureFactory;
        private Figure _currentFigure;
        private Resize.Resize _resize;

        private Point _lastPoint = Point.Empty;
        private Point _initialPosition = Point.Empty;

        private bool _isDragging = false;
        private bool _isFilling = false;
        private bool _isResizing = false;

        public DrawingForm(IReadOnlyList<FigureFactory> figureFactories)
        {
            InitializeComponent(); 
            DoubleBuffered = true;
            _commandManager = new CommandManager();
            _drawnFigures = new List<Figure>();
            _figureFactories = figureFactories;
            foreach (var figureFactory in _figureFactories)
                figureFactory.Finished += FigureFactoryFinished;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Z
                    && _commandManager.CanUndo)

                    _commandManager.Undo();

                else if (e.KeyCode == Keys.Y
                    && _commandManager.CanRedo)

                    _commandManager.Redo();
            }
            else if (e.KeyCode == Keys.Escape)
                _figureFactory = null;

            DrawingBox.Invalidate();
        }
 
        private void FigureFactoryFinished(Figure figure)
        {
            ICommand addCommand = new AddCommand(figure, _drawnFigures);
            _commandManager.AddCommand(addCommand);
            actionList.Items.Add($"Added {figure.GetType().Name} with area of {figure.CalcArea():F2}");
            
            _figureFactory = null;
            DrawingBox.Invalidate();
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {
            int buttonWidth = Width / (FigureTypes.Length + 1);
            for (int i = 0; i < FigureTypes.Length; i++)
            {
                Type figureType = FigureTypes[i];
                Button button = new Button
                {
                    Text = figureType.Name.Replace(FACTORY_SUFFIX, ""),
                    Height = WIDTH,
                    Width = buttonWidth,
                    Left = i * buttonWidth + WIDTH,
                    Top = Height - HEIGHT
                };

                FigureFactory factory = _figureFactories[i];
                button.Click += (object sender1, EventArgs e1) =>
                {
                    _figureFactory = factory;
                    _figureFactory.BeginCreateFigure();
                    _isFilling = false;
                    _isResizing = false;
                };
                Controls.Add(button);
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentFigure == null) 
                return;
            
            ICommand removeCommand = new DeleteCommand(_currentFigure, _drawnFigures);
            _commandManager.AddCommand(removeCommand);
            actionList.Items.Add($"Delete {_currentFigure.GetType().Name}");
            _currentFigure = null;
            DrawingBox.Invalidate();
        }

        private void ColorMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentFigure == null) 
                return;

            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.OK) 
                return;
            
            Color oldColor = _currentFigure.Color;
            Color newColor = colorDialog.Color;

            ICommand colorCommand = new ColorCommand(_currentFigure, oldColor, newColor);
            _commandManager.AddCommand(colorCommand);

            actionList.Items.Add($"Change {_currentFigure.GetType().Name} Color with {_currentFigure.Color.Name}");
            _currentFigure.Color = newColor;
            DrawingBox.Invalidate(); 
        }

        private void FillMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentFigure == null) 
                return;

            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.OK) 
                return;
            
            Color oldFilling = _currentFigure.FillColor;
            Color newFilling = colorDialog.Color;
            _isFilling = true;

            ICommand command = new FillCommand(_currentFigure, oldFilling, newFilling);
            _commandManager.AddCommand(command);

            actionList.Items.Add($"Change {_currentFigure.GetType().Name} Fill Color with {_currentFigure.FillColor.Name}");
            _currentFigure.FillColor = newFilling;
            DrawingBox.Invalidate(); 
        }

        private void AdditionalInfoMenuItem_Click(object sender, EventArgs e)
        {
            FigureInfo.FigureInfo figureInfo = new FigureInfo.FigureInfo(_drawnFigures,_currentFigure);
            AdditionalInfo additionalInfo = new AdditionalInfo(figureInfo);
            additionalInfo.ShowDialog();
        }

        private void ResizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isResizing = true;
            _resize = new Resize.Resize(_currentFigure.BoundingBox);
            Refresh();
        }

        private void DrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Figure figure in _drawnFigures)
            {
                if (!figure.Contains(e.Location))
                    continue;

                _currentFigure = figure;
                if (e.Button == MouseButtons.Left)
                {
                    _isDragging = true;
                    _lastPoint = e.Location;
                    _initialPosition = figure.Location;
                    return;
                }

                if (e.Button == MouseButtons.Right)
                {
                    _isResizing = false;
                    ContextMenuCommands.Show(DrawingBox, e.Location);
                    DrawingBox.Invalidate();
                    return;
                }
            }

            if (_figureFactory == null)
                return;

            _figureFactory.MouseDown(e);
            DrawingBox.Invalidate();
        }

        private void DrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizing && e.Button == MouseButtons.Left)
            {
                _resize.ResizeTo(e.Location,_currentFigure);
                Refresh();
                return;
            }

            if (_isDragging)
            {
                Point delta = new Point(e.X - _lastPoint.X, e.Y - _lastPoint.Y);
                _lastPoint = e.Location;
                _currentFigure.Move(delta);
                DrawingBox.Invalidate();
                return;
            }

            if (_figureFactory == null) 
                return;

            _figureFactory.MouseMove(e);
            DrawingBox.Invalidate();
        }

        private void DrawingBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                ICommand moveCommand = new MoveCommand(_currentFigure, _initialPosition, _currentFigure.Location);
                _commandManager.AddCommand(moveCommand);
                actionList.Items.Add($"Move {_currentFigure.GetType().Name}");
                _isDragging = false;
                _lastPoint = Point.Empty;
                Invalidate();
                return;
            }

            if (_figureFactory == null) 
                return;

            _figureFactory.MouseUp(e);
            DrawingBox.Invalidate();

            if (!_isResizing)
                return;

            _isResizing = false;
            Refresh();
        }
        [DebuggerStepThrough]
        private void DrawingBox_Paint(object sender, PaintEventArgs e)
        {
            _figureFactory?.Draw(e.Graphics); 
            foreach (Figure figure in _drawnFigures)
            {
                figure.Draw(e.Graphics);
                if (_isFilling)
                    figure.Fill(e.Graphics);
                if (_isResizing)
                    e.Graphics.DrawRectangle(_dashedPen, _resize.BoundingBox);
            }
        }

        private void Redo_Btn_Click(object sender, EventArgs e)
        {
            if (!_commandManager.CanRedo) 
                return;

            _isResizing = false;
            _commandManager.Redo();
            DrawingBox.Invalidate();
        }

        private void Undo_Btn_Click(object sender, EventArgs e)
        {
            if (!_commandManager.CanUndo) 
                return;

            _isResizing = false;
            _commandManager.Undo();
            DrawingBox.Invalidate();
        }

        private void SaveToFile_Click(object sender, EventArgs e)
        {
            DrawingData drawingData = new DrawingData { DrawnFigures = _drawnFigures };

            try
            {
                string json = JsonConvert.SerializeObject(drawingData, Formatting.Indented, _jsonSettings);
                File.WriteAllText(PATH, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}");
                return;
            }

            MessageBox.Show("File saved successfully.");
            actionList.Items.Add("Save figures to file");
        }

        private void LoadFromFile_Click(object sender, EventArgs e)
        {
            DrawingData drawingData;

            try
            {
                string json = File.ReadAllText(PATH);
                drawingData = JsonConvert.DeserializeObject<DrawingData>(json, _jsonSettings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}");
                return;
            }

            ICommand loadCommand = new LoadCommand(_drawnFigures, drawingData.DrawnFigures);
            _commandManager.AddCommand(loadCommand);
            DrawingBox.Invalidate();
            MessageBox.Show("File loaded successfully.");
            actionList.Items.Add("Load figures from file");
        }
    }
}
