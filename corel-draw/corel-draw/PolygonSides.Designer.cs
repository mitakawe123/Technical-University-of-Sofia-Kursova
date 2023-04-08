namespace corel_draw
{
    partial class PolygonSides
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
            this.DrawPolygon = new System.Windows.Forms.Button();
            this.Polygon_Sides = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the number of sides you want your polygon to have";
            // 
            // DrawPolygon
            // 
            this.DrawPolygon.Location = new System.Drawing.Point(171, 146);
            this.DrawPolygon.Name = "DrawPolygon";
            this.DrawPolygon.Size = new System.Drawing.Size(185, 75);
            this.DrawPolygon.TabIndex = 1;
            this.DrawPolygon.Text = "Start Drawing";
            this.DrawPolygon.UseVisualStyleBackColor = true;
            this.DrawPolygon.Click += new System.EventHandler(this.DrawPolygon_Click);
            // 
            // Polygon_Sides
            // 
            this.Polygon_Sides.Location = new System.Drawing.Point(171, 83);
            this.Polygon_Sides.Multiline = true;
            this.Polygon_Sides.Name = "Polygon_Sides";
            this.Polygon_Sides.Size = new System.Drawing.Size(185, 42);
            this.Polygon_Sides.TabIndex = 2;
            // 
            // PolygonSides
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 254);
            this.Controls.Add(this.Polygon_Sides);
            this.Controls.Add(this.DrawPolygon);
            this.Controls.Add(this.label1);
            this.Name = "PolygonSides";
            this.Text = "PolygonSides";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DrawPolygon;
        private System.Windows.Forms.TextBox Polygon_Sides;
    }
}