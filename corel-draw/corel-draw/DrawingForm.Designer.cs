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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Undo_Btn = new System.Windows.Forms.Button();
            this.Redo_Btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(32, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1012, 411);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Undo_Btn
            // 
            this.Undo_Btn.Location = new System.Drawing.Point(0, 0);
            this.Undo_Btn.Name = "Undo_Btn";
            this.Undo_Btn.Size = new System.Drawing.Size(75, 23);
            this.Undo_Btn.TabIndex = 7;
            this.Undo_Btn.Text = "UNDO";
            this.Undo_Btn.UseVisualStyleBackColor = true;
            this.Undo_Btn.Click += new System.EventHandler(this.Undo_Btn_Click);
            // 
            // Redo_Btn
            // 
            this.Redo_Btn.Location = new System.Drawing.Point(71, 0);
            this.Redo_Btn.Name = "Redo_Btn";
            this.Redo_Btn.Size = new System.Drawing.Size(75, 23);
            this.Redo_Btn.TabIndex = 8;
            this.Redo_Btn.Text = "REDO";
            this.Redo_Btn.UseVisualStyleBackColor = true;
            this.Redo_Btn.Click += new System.EventHandler(this.Redo_Btn_Click);
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 633);
            this.Controls.Add(this.Redo_Btn);
            this.Controls.Add(this.Undo_Btn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "DrawingForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.DrawingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Undo_Btn;
        private System.Windows.Forms.Button Redo_Btn;
    }
}

