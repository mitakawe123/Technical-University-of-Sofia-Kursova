namespace corel_draw
{
    partial class CalculationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.X_Input = new System.Windows.Forms.TextBox();
            this.Header = new System.Windows.Forms.Label();
            this.Draw_Button = new System.Windows.Forms.Button();
            this.X_Label = new System.Windows.Forms.Label();
            this.Y_Label = new System.Windows.Forms.Label();
            this.Height_Label = new System.Windows.Forms.Label();
            this.Width_Label = new System.Windows.Forms.Label();
            this.Y_Input = new System.Windows.Forms.TextBox();
            this.Height_Input = new System.Windows.Forms.TextBox();
            this.Width_Input = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // X_Input
            // 
            this.X_Input.Location = new System.Drawing.Point(271, 128);
            this.X_Input.Multiline = true;
            this.X_Input.Name = "X_Input";
            this.X_Input.Size = new System.Drawing.Size(227, 26);
            this.X_Input.TabIndex = 0;
            // 
            // Header
            // 
            this.Header.AutoSize = true;
            this.Header.Location = new System.Drawing.Point(268, 79);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(250, 13);
            this.Header.TabIndex = 1;
            this.Header.Text = "Put measurements in the fields and press the button";
            // 
            // Draw_Button
            // 
            this.Draw_Button.Location = new System.Drawing.Point(296, 361);
            this.Draw_Button.Name = "Draw_Button";
            this.Draw_Button.Size = new System.Drawing.Size(177, 71);
            this.Draw_Button.TabIndex = 4;
            this.Draw_Button.Text = "Draw";
            this.Draw_Button.UseVisualStyleBackColor = true;
            this.Draw_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // X_Label
            // 
            this.X_Label.AutoSize = true;
            this.X_Label.Location = new System.Drawing.Point(199, 131);
            this.X_Label.Name = "X_Label";
            this.X_Label.Size = new System.Drawing.Size(36, 13);
            this.X_Label.TabIndex = 3;
            this.X_Label.Text = "X Axis";
            // 
            // Y_Label
            // 
            this.Y_Label.AutoSize = true;
            this.Y_Label.Location = new System.Drawing.Point(199, 181);
            this.Y_Label.Name = "Y_Label";
            this.Y_Label.Size = new System.Drawing.Size(36, 13);
            this.Y_Label.TabIndex = 4;
            this.Y_Label.Text = "Y Axis";
            // 
            // Height_Label
            // 
            this.Height_Label.AutoSize = true;
            this.Height_Label.Location = new System.Drawing.Point(199, 287);
            this.Height_Label.Name = "Height_Label";
            this.Height_Label.Size = new System.Drawing.Size(38, 13);
            this.Height_Label.TabIndex = 5;
            this.Height_Label.Text = "Height";
            // 
            // Width_Label
            // 
            this.Width_Label.AutoSize = true;
            this.Width_Label.Location = new System.Drawing.Point(199, 242);
            this.Width_Label.Name = "Width_Label";
            this.Width_Label.Size = new System.Drawing.Size(35, 13);
            this.Width_Label.TabIndex = 6;
            this.Width_Label.Text = "Width";
            // 
            // Y_Input
            // 
            this.Y_Input.Location = new System.Drawing.Point(271, 178);
            this.Y_Input.Multiline = true;
            this.Y_Input.Name = "Y_Input";
            this.Y_Input.Size = new System.Drawing.Size(227, 26);
            this.Y_Input.TabIndex = 1;
            // 
            // Height_Input
            // 
            this.Height_Input.Location = new System.Drawing.Point(271, 278);
            this.Height_Input.Multiline = true;
            this.Height_Input.Name = "Height_Input";
            this.Height_Input.Size = new System.Drawing.Size(227, 31);
            this.Height_Input.TabIndex = 3;
            // 
            // Width_Input
            // 
            this.Width_Input.Location = new System.Drawing.Point(271, 226);
            this.Width_Input.Multiline = true;
            this.Width_Input.Name = "Width_Input";
            this.Width_Input.Size = new System.Drawing.Size(227, 29);
            this.Width_Input.TabIndex = 2;
            // 
            // CalculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Width_Input);
            this.Controls.Add(this.Height_Input);
            this.Controls.Add(this.Y_Input);
            this.Controls.Add(this.Width_Label);
            this.Controls.Add(this.Height_Label);
            this.Controls.Add(this.Y_Label);
            this.Controls.Add(this.X_Label);
            this.Controls.Add(this.Draw_Button);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.X_Input);
            this.Name = "CalculationForm";
            this.Text = "CalculationForm";
            this.Load += new System.EventHandler(this.CalculationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox X_Input;
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.Button Draw_Button;
        private System.Windows.Forms.Label X_Label;
        private System.Windows.Forms.Label Y_Label;
        private System.Windows.Forms.Label Height_Label;
        private System.Windows.Forms.Label Width_Label;
        private System.Windows.Forms.TextBox Y_Input;
        private System.Windows.Forms.TextBox Height_Input;
        private System.Windows.Forms.TextBox Width_Input;
    }
}