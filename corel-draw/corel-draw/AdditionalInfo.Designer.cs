namespace corel_draw
{
    partial class AdditionalInfo
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
            this.Close_btn = new System.Windows.Forms.Button();
            this.Figure_type = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Fill_color = new System.Windows.Forms.Label();
            this.type_info = new System.Windows.Forms.Label();
            this.border_info = new System.Windows.Forms.Label();
            this.fill_info = new System.Windows.Forms.Label();
            this.area_info = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.additional_info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Close_btn
            // 
            this.Close_btn.Location = new System.Drawing.Point(107, 175);
            this.Close_btn.Name = "Close_btn";
            this.Close_btn.Size = new System.Drawing.Size(137, 61);
            this.Close_btn.TabIndex = 0;
            this.Close_btn.Text = "CLOSE";
            this.Close_btn.UseVisualStyleBackColor = true;
            this.Close_btn.Click += new System.EventHandler(this.Close_btn_Click);
            // 
            // Figure_type
            // 
            this.Figure_type.AutoSize = true;
            this.Figure_type.Location = new System.Drawing.Point(48, 4);
            this.Figure_type.Name = "Figure_type";
            this.Figure_type.Size = new System.Drawing.Size(75, 13);
            this.Figure_type.TabIndex = 1;
            this.Figure_type.Text = "Type of figure:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Border Color of figure:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Area of figure:";
            // 
            // Fill_color
            // 
            this.Fill_color.AutoSize = true;
            this.Fill_color.Location = new System.Drawing.Point(50, 77);
            this.Fill_color.Name = "Fill_color";
            this.Fill_color.Size = new System.Drawing.Size(90, 13);
            this.Fill_color.TabIndex = 4;
            this.Fill_color.Text = "Fill Color of figure:";
            // 
            // type_info
            // 
            this.type_info.AutoSize = true;
            this.type_info.Location = new System.Drawing.Point(247, 4);
            this.type_info.Name = "type_info";
            this.type_info.Size = new System.Drawing.Size(35, 13);
            this.type_info.TabIndex = 5;
            this.type_info.Text = "label3";
            // 
            // border_info
            // 
            this.border_info.AutoSize = true;
            this.border_info.Location = new System.Drawing.Point(247, 39);
            this.border_info.Name = "border_info";
            this.border_info.Size = new System.Drawing.Size(35, 13);
            this.border_info.TabIndex = 6;
            this.border_info.Text = "label4";
            // 
            // fill_info
            // 
            this.fill_info.AutoSize = true;
            this.fill_info.Location = new System.Drawing.Point(247, 77);
            this.fill_info.Name = "fill_info";
            this.fill_info.Size = new System.Drawing.Size(35, 13);
            this.fill_info.TabIndex = 7;
            this.fill_info.Text = "label5";
            // 
            // area_info
            // 
            this.area_info.AutoSize = true;
            this.area_info.Location = new System.Drawing.Point(247, 113);
            this.area_info.Name = "area_info";
            this.area_info.Size = new System.Drawing.Size(35, 13);
            this.area_info.TabIndex = 8;
            this.area_info.Text = "label6";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Special Prop";
            // 
            // additional_info
            // 
            this.additional_info.AutoSize = true;
            this.additional_info.Location = new System.Drawing.Point(247, 148);
            this.additional_info.Name = "additional_info";
            this.additional_info.Size = new System.Drawing.Size(35, 13);
            this.additional_info.TabIndex = 10;
            this.additional_info.Text = "label4";
            // 
            // AdditionalInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 245);
            this.Controls.Add(this.additional_info);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.area_info);
            this.Controls.Add(this.fill_info);
            this.Controls.Add(this.border_info);
            this.Controls.Add(this.type_info);
            this.Controls.Add(this.Fill_color);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Figure_type);
            this.Controls.Add(this.Close_btn);
            this.Name = "AdditionalInfo";
            this.Text = "AdditionalInfo";
            this.Load += new System.EventHandler(this.AdditionalInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Close_btn;
        private System.Windows.Forms.Label Figure_type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Fill_color;
        private System.Windows.Forms.Label type_info;
        private System.Windows.Forms.Label border_info;
        private System.Windows.Forms.Label fill_info;
        private System.Windows.Forms.Label area_info;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label additional_info;
    }
}