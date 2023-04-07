namespace corel_draw
{
    partial class DrawingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.DrawingBox = new System.Windows.Forms.PictureBox();
            this.Undo_Btn = new System.Windows.Forms.Button();
            this.Redo_Btn = new System.Windows.Forms.Button();
            this.actionList = new System.Windows.Forms.ListBox();
            this.SaveToFile = new System.Windows.Forms.Button();
            this.LoadFromFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(478, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "What do you want to draw";
            // 
            // DrawingBox
            // 
            this.DrawingBox.Location = new System.Drawing.Point(32, 93);
            this.DrawingBox.Name = "DrawingBox";
            this.DrawingBox.Size = new System.Drawing.Size(1012, 411);
            this.DrawingBox.TabIndex = 6;
            this.DrawingBox.TabStop = false;
            this.DrawingBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingBox_Paint);
            this.DrawingBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingBox_MouseDown);
            this.DrawingBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingBox_MouseMove);
            this.DrawingBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingBox_MouseUp);
            // 
            // Undo_Btn
            // 
            this.Undo_Btn.Location = new System.Drawing.Point(0, 0);
            this.Undo_Btn.Name = "Undo_Btn";
            this.Undo_Btn.Size = new System.Drawing.Size(75, 41);
            this.Undo_Btn.TabIndex = 7;
            this.Undo_Btn.Text = "UNDO";
            this.Undo_Btn.UseVisualStyleBackColor = true;
            this.Undo_Btn.Click += new System.EventHandler(this.Undo_Btn_Click);
            // 
            // Redo_Btn
            // 
            this.Redo_Btn.Location = new System.Drawing.Point(71, 0);
            this.Redo_Btn.Name = "Redo_Btn";
            this.Redo_Btn.Size = new System.Drawing.Size(75, 41);
            this.Redo_Btn.TabIndex = 8;
            this.Redo_Btn.Text = "REDO";
            this.Redo_Btn.UseVisualStyleBackColor = true;
            this.Redo_Btn.Click += new System.EventHandler(this.Redo_Btn_Click);
            // 
            // actionList
            // 
            this.actionList.FormattingEnabled = true;
            this.actionList.Location = new System.Drawing.Point(1068, 93);
            this.actionList.Name = "actionList";
            this.actionList.Size = new System.Drawing.Size(213, 407);
            this.actionList.TabIndex = 9;
            // 
            // SaveToFile
            // 
            this.SaveToFile.Location = new System.Drawing.Point(1239, 0);
            this.SaveToFile.Name = "SaveToFile";
            this.SaveToFile.Size = new System.Drawing.Size(104, 41);
            this.SaveToFile.TabIndex = 10;
            this.SaveToFile.Text = "Save to File";
            this.SaveToFile.UseVisualStyleBackColor = true;
            this.SaveToFile.Click += new System.EventHandler(this.SaveToFile_Click);
            // 
            // LoadFromFile
            // 
            this.LoadFromFile.Location = new System.Drawing.Point(1143, 0);
            this.LoadFromFile.Name = "LoadFromFile";
            this.LoadFromFile.Size = new System.Drawing.Size(99, 41);
            this.LoadFromFile.TabIndex = 11;
            this.LoadFromFile.Text = "Load from File";
            this.LoadFromFile.UseVisualStyleBackColor = true;
            this.LoadFromFile.Click += new System.EventHandler(this.LoadFromFile_Click);
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 630);
            this.Controls.Add(this.LoadFromFile);
            this.Controls.Add(this.SaveToFile);
            this.Controls.Add(this.actionList);
            this.Controls.Add(this.Redo_Btn);
            this.Controls.Add(this.Undo_Btn);
            this.Controls.Add(this.DrawingBox);
            this.Controls.Add(this.label1);
            this.Name = "DrawingForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.DrawingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DrawingBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox DrawingBox;
        private System.Windows.Forms.Button Undo_Btn;
        private System.Windows.Forms.Button Redo_Btn;
        private System.Windows.Forms.ListBox actionList;
        private System.Windows.Forms.Button SaveToFile;
        private System.Windows.Forms.Button LoadFromFile;
    }
}

