using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corel_draw
{
    public partial class PolygonTypeForm : Form
    {
        private int _sides;
        public PolygonTypeForm(int sides)
        {
            InitializeComponent();
            _sides = sides;
        }

        private void PolygonTypeForm_Load(object sender, EventArgs e)
        {
            if (_sides == 2) Drawing_Type.Text = "Enter the X and Y coordinates for Line";
            else if (_sides == 3) Drawing_Type.Text = "Enter the X and Y coordinates for Triangle";
            else Drawing_Type.Text = "Enter the X and Y coordinates for Polygon";

            int controlWidth = 300;
            int controlHeight = 30;
            int topMargin = (ClientSize.Height - (controlHeight * _sides)) / 2;
            int leftMargin = (ClientSize.Width - (controlWidth + 200)) / 2;

            for (int i = 0; i < _sides; i++)
            {
                System.Windows.Forms.Label labelX = new System.Windows.Forms.Label();
                labelX.Text = $"Position X{i + 1}:";
                labelX.Top = topMargin + (i * controlHeight);
                labelX.Left = leftMargin;
                labelX.Width = 100;
                Controls.Add(labelX);

                TextBox inputTextBoxX = new TextBox();
                inputTextBoxX.Top = topMargin + (i * controlHeight);
                inputTextBoxX.Left = leftMargin + 100;
                inputTextBoxX.Width = controlWidth / 2;
                Controls.Add(inputTextBoxX);

                System.Windows.Forms.Label labelY = new System.Windows.Forms.Label();
                labelY.Text = $"Position Y{i + 1}:";
                labelY.Top = topMargin + (i * controlHeight);
                labelY.Left = leftMargin + controlWidth / 2 + 100;
                labelY.Width = 100;
                Controls.Add(labelY);

                TextBox inputTextBoxY = new TextBox();
                inputTextBoxY.Top = topMargin + (i * controlHeight);
                inputTextBoxY.Left = leftMargin + controlWidth / 2 + 200;
                inputTextBoxY.Width = controlWidth / 2;
                Controls.Add(inputTextBoxY);
            }
        }
    }
}
