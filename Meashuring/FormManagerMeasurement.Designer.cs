namespace ImageProcessor.Meashuring
{
    partial class FormManagerMeasurement
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
            this.panel0 = new System.Windows.Forms.Panel();
            this.panelProfile = new System.Windows.Forms.Panel();
            this.controlProphiles1 = new ImageProcessor3D.ControlProphiles();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelManager = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblShowProphiles = new System.Windows.Forms.Label();
            this.panelType = new System.Windows.Forms.Panel();
            this.cbRectangle = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCircle = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLine = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPoint = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel0.SuspendLayout();
            this.panelProfile.SuspendLayout();
            this.panelManager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.panelType.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel0
            // 
            this.panel0.Controls.Add(this.panelProfile);
            this.panel0.Controls.Add(this.splitter1);
            this.panel0.Controls.Add(this.panelManager);
            this.panel0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel0.Location = new System.Drawing.Point(0, 0);
            this.panel0.Name = "panel0";
            this.panel0.Size = new System.Drawing.Size(222, 217);
            this.panel0.TabIndex = 0;
            // 
            // panelProfile
            // 
            this.panelProfile.Controls.Add(this.controlProphiles1);
            this.panelProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProfile.Location = new System.Drawing.Point(224, 0);
            this.panelProfile.Name = "panelProfile";
            this.panelProfile.Size = new System.Drawing.Size(0, 217);
            this.panelProfile.TabIndex = 2;
            // 
            // controlProphiles1
            // 
            this.controlProphiles1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlProphiles1.Location = new System.Drawing.Point(0, 0);
            this.controlProphiles1.Name = "controlProphiles1";
            this.controlProphiles1.Size = new System.Drawing.Size(0, 217);
            this.controlProphiles1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(221, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 217);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panelManager
            // 
            this.panelManager.Controls.Add(this.dataGridView1);
            this.panelManager.Controls.Add(this.panelBottom);
            this.panelManager.Controls.Add(this.panelType);
            this.panelManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelManager.Location = new System.Drawing.Point(0, 0);
            this.panelManager.Name = "panelManager";
            this.panelManager.Size = new System.Drawing.Size(221, 217);
            this.panelManager.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(221, 190);
            this.dataGridView1.TabIndex = 2;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblShowProphiles);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 203);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(221, 14);
            this.panelBottom.TabIndex = 1;
            // 
            // lblShowProphiles
            // 
            this.lblShowProphiles.AutoSize = true;
            this.lblShowProphiles.BackColor = System.Drawing.SystemColors.Info;
            this.lblShowProphiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblShowProphiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblShowProphiles.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblShowProphiles.Location = new System.Drawing.Point(0, 0);
            this.lblShowProphiles.Name = "lblShowProphiles";
            this.lblShowProphiles.Size = new System.Drawing.Size(69, 12);
            this.lblShowProphiles.TabIndex = 81;
            this.lblShowProphiles.Text = "Show Prophiles";
            this.lblShowProphiles.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblShowProphiles.Click += new System.EventHandler(this.lblShowProphiles_Click);
            // 
            // panelType
            // 
            this.panelType.Controls.Add(this.cbRectangle);
            this.panelType.Controls.Add(this.label3);
            this.panelType.Controls.Add(this.cbCircle);
            this.panelType.Controls.Add(this.label2);
            this.panelType.Controls.Add(this.cbLine);
            this.panelType.Controls.Add(this.label1);
            this.panelType.Controls.Add(this.cbPoint);
            this.panelType.Controls.Add(this.label8);
            this.panelType.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelType.Location = new System.Drawing.Point(0, 0);
            this.panelType.Name = "panelType";
            this.panelType.Size = new System.Drawing.Size(221, 13);
            this.panelType.TabIndex = 0;
            // 
            // cbRectangle
            // 
            this.cbRectangle.BackColor = System.Drawing.SystemColors.Highlight;
            this.cbRectangle.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbRectangle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRectangle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbRectangle.ForeColor = System.Drawing.SystemColors.Info;
            this.cbRectangle.Location = new System.Drawing.Point(160, 0);
            this.cbRectangle.Name = "cbRectangle";
            this.cbRectangle.Size = new System.Drawing.Size(12, 13);
            this.cbRectangle.TabIndex = 143;
            this.cbRectangle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRectangle.UseVisualStyleBackColor = false;
            this.cbRectangle.CheckedChanged += new System.EventHandler(this.cbRectangle_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Highlight;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.Info;
            this.label3.Location = new System.Drawing.Point(113, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 142;
            this.label3.Text = "Rectangle";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbCircle
            // 
            this.cbCircle.BackColor = System.Drawing.SystemColors.Highlight;
            this.cbCircle.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCircle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbCircle.ForeColor = System.Drawing.SystemColors.Info;
            this.cbCircle.Location = new System.Drawing.Point(101, 0);
            this.cbCircle.Name = "cbCircle";
            this.cbCircle.Size = new System.Drawing.Size(12, 13);
            this.cbCircle.TabIndex = 141;
            this.cbCircle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbCircle.UseVisualStyleBackColor = false;
            this.cbCircle.CheckedChanged += new System.EventHandler(this.cbCircle_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Highlight;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(72, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 140;
            this.label2.Text = "Circle";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbLine
            // 
            this.cbLine.BackColor = System.Drawing.SystemColors.Highlight;
            this.cbLine.Checked = true;
            this.cbLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLine.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbLine.ForeColor = System.Drawing.SystemColors.Info;
            this.cbLine.Location = new System.Drawing.Point(60, 0);
            this.cbLine.Name = "cbLine";
            this.cbLine.Size = new System.Drawing.Size(12, 13);
            this.cbLine.TabIndex = 139;
            this.cbLine.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbLine.UseVisualStyleBackColor = false;
            this.cbLine.CheckedChanged += new System.EventHandler(this.cbLine_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Highlight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(38, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 12);
            this.label1.TabIndex = 138;
            this.label1.Text = "Line";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbPoint
            // 
            this.cbPoint.BackColor = System.Drawing.SystemColors.Highlight;
            this.cbPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbPoint.ForeColor = System.Drawing.SystemColors.Info;
            this.cbPoint.Location = new System.Drawing.Point(26, 0);
            this.cbPoint.Name = "cbPoint";
            this.cbPoint.Size = new System.Drawing.Size(12, 13);
            this.cbPoint.TabIndex = 137;
            this.cbPoint.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbPoint.UseVisualStyleBackColor = false;
            this.cbPoint.CheckedChanged += new System.EventHandler(this.cbPoint_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Highlight;
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.SystemColors.Info;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 12);
            this.label8.TabIndex = 136;
            this.label8.Text = "Point";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FormManagerMeasurement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 217);
            this.Controls.Add(this.panel0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Location = new System.Drawing.Point(20, 100);
            this.Name = "FormManagerMeasurement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manager of Measurements";
            this.TopMost = true;
            this.panel0.ResumeLayout(false);
            this.panelProfile.ResumeLayout(false);
            this.panelManager.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelType.ResumeLayout(false);
            this.panelType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel0;
        private System.Windows.Forms.Panel panelManager;
        private System.Windows.Forms.Panel panelProfile;
        private ImageProcessor3D.ControlProphiles controlProphiles1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelType;
        private System.Windows.Forms.CheckBox cbRectangle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbCircle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbPoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblShowProphiles;

    }
}