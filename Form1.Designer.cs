namespace WorkStudyUI
{
    partial class Form1
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
            this.btnFilePathIn = new System.Windows.Forms.Button();
            this.lblExcelFilePrompt = new System.Windows.Forms.Label();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.btnParseFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblTextFilePrompt = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnFilePathOut = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFilePathIn
            // 
            this.btnFilePathIn.Location = new System.Drawing.Point(400, 38);
            this.btnFilePathIn.Name = "btnFilePathIn";
            this.btnFilePathIn.Size = new System.Drawing.Size(75, 23);
            this.btnFilePathIn.TabIndex = 2;
            this.btnFilePathIn.Text = "Browse";
            this.btnFilePathIn.UseVisualStyleBackColor = true;
            this.btnFilePathIn.Click += new System.EventHandler(this.btnFilePathIn_Click);
            // 
            // lblExcelFilePrompt
            // 
            this.lblExcelFilePrompt.AutoSize = true;
            this.lblExcelFilePrompt.Location = new System.Drawing.Point(7, 20);
            this.lblExcelFilePrompt.Name = "lblExcelFilePrompt";
            this.lblExcelFilePrompt.Size = new System.Drawing.Size(404, 13);
            this.lblExcelFilePrompt.TabIndex = 0;
            this.lblExcelFilePrompt.Text = "Please select input MS Excel (.xls/.xlsx) OR Comma-Separated Value (.csv) data fi" +
    "le:";
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(10, 40);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(373, 20);
            this.txtInputFile.TabIndex = 1;
            // 
            // btnParseFile
            // 
            this.btnParseFile.Location = new System.Drawing.Point(10, 120);
            this.btnParseFile.Name = "btnParseFile";
            this.btnParseFile.Size = new System.Drawing.Size(465, 23);
            this.btnParseFile.TabIndex = 6;
            this.btnParseFile.Text = "OK";
            this.btnParseFile.UseVisualStyleBackColor = true;
            this.btnParseFile.Click += new System.EventHandler(this.parseFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblTextFilePrompt
            // 
            this.lblTextFilePrompt.AutoSize = true;
            this.lblTextFilePrompt.Location = new System.Drawing.Point(7, 70);
            this.lblTextFilePrompt.Name = "lblTextFilePrompt";
            this.lblTextFilePrompt.Size = new System.Drawing.Size(218, 13);
            this.lblTextFilePrompt.TabIndex = 3;
            this.lblTextFilePrompt.Text = "Enter desired output path and file name (.txt):";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(10, 90);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(373, 20);
            this.txtOutputFile.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(6, 19);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(453, 230);
            this.textBox3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Location = new System.Drawing.Point(10, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 255);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Text File Output";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 414);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(484, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnFilePathOut
            // 
            this.btnFilePathOut.Location = new System.Drawing.Point(400, 88);
            this.btnFilePathOut.Name = "btnFilePathOut";
            this.btnFilePathOut.Size = new System.Drawing.Size(75, 23);
            this.btnFilePathOut.TabIndex = 5;
            this.btnFilePathOut.Text = "Browse";
            this.btnFilePathOut.UseVisualStyleBackColor = true;
            this.btnFilePathOut.Click += new System.EventHandler(this.btnFilePathOut_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 436);
            this.Controls.Add(this.btnFilePathOut);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.lblTextFilePrompt);
            this.Controls.Add(this.btnParseFile);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.lblExcelFilePrompt);
            this.Controls.Add(this.btnFilePathIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "UCSF Work Study UI Application v.2.0 © Michael Tang";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFilePathIn;
        private System.Windows.Forms.Label lblExcelFilePrompt;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.Button btnParseFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblTextFilePrompt;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnFilePathOut;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

