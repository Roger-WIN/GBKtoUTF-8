namespace WinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_file = new System.Windows.Forms.Label();
            this.label_directory = new System.Windows.Forms.Label();
            this.label_output = new System.Windows.Forms.Label();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.textBox_directory = new System.Windows.Forms.TextBox();
            this.textBox_output = new System.Windows.Forms.TextBox();
            this.button_file = new System.Windows.Forms.Button();
            this.button_chooseDirectory = new System.Windows.Forms.Button();
            this.button_chooseOutput = new System.Windows.Forms.Button();
            this.button_openDirectory = new System.Windows.Forms.Button();
            this.button_openOutput = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox_bom = new System.Windows.Forms.CheckBox();
            this.checkBox_recur = new System.Windows.Forms.CheckBox();
            this.checkBox_override = new System.Windows.Forms.CheckBox();
            this.checkBox_sameOutput = new System.Windows.Forms.CheckBox();
            this.checkBox_openOutput = new System.Windows.Forms.CheckBox();
            this.button_convert = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.label_file, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_directory, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_output, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox_file, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_directory, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_output, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_file, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_chooseDirectory, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_chooseOutput, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_openDirectory, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_openOutput, 3, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(21, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1083, 296);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label_file
            // 
            this.label_file.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_file.AutoSize = true;
            this.label_file.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_file.Location = new System.Drawing.Point(3, 34);
            this.label_file.Name = "label_file";
            this.label_file.Size = new System.Drawing.Size(97, 30);
            this.label_file.TabIndex = 1;
            this.label_file.Text = "转换文件";
            this.label_file.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_directory
            // 
            this.label_directory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_directory.AutoSize = true;
            this.label_directory.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_directory.Location = new System.Drawing.Point(3, 132);
            this.label_directory.Name = "label_directory";
            this.label_directory.Size = new System.Drawing.Size(118, 30);
            this.label_directory.TabIndex = 2;
            this.label_directory.Text = "转换文件夹";
            this.label_directory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_output
            // 
            this.label_output.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_output.AutoSize = true;
            this.label_output.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_output.Location = new System.Drawing.Point(3, 231);
            this.label_output.Name = "label_output";
            this.label_output.Size = new System.Drawing.Size(118, 30);
            this.label_output.TabIndex = 3;
            this.label_output.Text = "输出文件夹";
            this.label_output.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_file
            // 
            this.textBox_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_file.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_file.Location = new System.Drawing.Point(165, 31);
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.ReadOnly = true;
            this.textBox_file.Size = new System.Drawing.Size(697, 35);
            this.textBox_file.TabIndex = 4;
            // 
            // textBox_directory
            // 
            this.textBox_directory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_directory.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_directory.Location = new System.Drawing.Point(165, 129);
            this.textBox_directory.Name = "textBox_directory";
            this.textBox_directory.ReadOnly = true;
            this.textBox_directory.Size = new System.Drawing.Size(697, 35);
            this.textBox_directory.TabIndex = 5;
            // 
            // textBox_output
            // 
            this.textBox_output.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_output.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_output.Location = new System.Drawing.Point(165, 228);
            this.textBox_output.Name = "textBox_output";
            this.textBox_output.ReadOnly = true;
            this.textBox_output.Size = new System.Drawing.Size(697, 35);
            this.textBox_output.TabIndex = 6;
            // 
            // button_file
            // 
            this.button_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_file.AutoSize = true;
            this.button_file.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_file.Location = new System.Drawing.Point(868, 29);
            this.button_file.Name = "button_file";
            this.button_file.Size = new System.Drawing.Size(102, 40);
            this.button_file.TabIndex = 7;
            this.button_file.Text = "选择";
            this.button_file.Click += new System.EventHandler(this.button_file_Click);
            // 
            // button_chooseDirectory
            // 
            this.button_chooseDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_chooseDirectory.AutoSize = true;
            this.button_chooseDirectory.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_chooseDirectory.Location = new System.Drawing.Point(868, 127);
            this.button_chooseDirectory.Name = "button_chooseDirectory";
            this.button_chooseDirectory.Size = new System.Drawing.Size(102, 40);
            this.button_chooseDirectory.TabIndex = 8;
            this.button_chooseDirectory.Text = "选择";
            this.button_chooseDirectory.Click += new System.EventHandler(this.button_chooseDirectory_Click);
            // 
            // button_chooseOutput
            // 
            this.button_chooseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_chooseOutput.AutoSize = true;
            this.button_chooseOutput.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_chooseOutput.Location = new System.Drawing.Point(868, 226);
            this.button_chooseOutput.Name = "button_chooseOutput";
            this.button_chooseOutput.Size = new System.Drawing.Size(102, 40);
            this.button_chooseOutput.TabIndex = 9;
            this.button_chooseOutput.Text = "选择";
            this.button_chooseOutput.Click += new System.EventHandler(this.button_chooseOutput_Click);
            // 
            // button_openDirectory
            // 
            this.button_openDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_openDirectory.AutoSize = true;
            this.button_openDirectory.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_openDirectory.Location = new System.Drawing.Point(976, 127);
            this.button_openDirectory.Name = "button_openDirectory";
            this.button_openDirectory.Size = new System.Drawing.Size(104, 40);
            this.button_openDirectory.TabIndex = 10;
            this.button_openDirectory.Text = "打开";
            this.button_openDirectory.Click += new System.EventHandler(this.button_openDirectory_Click);
            // 
            // button_openOutput
            // 
            this.button_openOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_openOutput.AutoSize = true;
            this.button_openOutput.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_openOutput.Location = new System.Drawing.Point(976, 226);
            this.button_openOutput.Name = "button_openOutput";
            this.button_openOutput.Size = new System.Drawing.Size(104, 40);
            this.button_openOutput.TabIndex = 11;
            this.button_openOutput.Text = "打开";
            this.button_openOutput.Click += new System.EventHandler(this.button_openOutput_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.checkBox_bom, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBox_recur, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBox_override, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBox_sameOutput, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBox_openOutput, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_convert, 5, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(21, 349);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1083, 54);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // checkBox_bom
            // 
            this.checkBox_bom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox_bom.AutoSize = true;
            this.checkBox_bom.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_bom.Location = new System.Drawing.Point(3, 10);
            this.checkBox_bom.Name = "checkBox_bom";
            this.checkBox_bom.Size = new System.Drawing.Size(134, 34);
            this.checkBox_bom.TabIndex = 13;
            this.checkBox_bom.Text = "提供 BOM";
            // 
            // checkBox_recur
            // 
            this.checkBox_recur.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox_recur.AutoSize = true;
            this.checkBox_recur.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_recur.Location = new System.Drawing.Point(143, 10);
            this.checkBox_recur.Name = "checkBox_recur";
            this.checkBox_recur.Size = new System.Drawing.Size(165, 34);
            this.checkBox_recur.TabIndex = 14;
            this.checkBox_recur.Text = "包含子文件夹";
            // 
            // checkBox_override
            // 
            this.checkBox_override.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox_override.AutoSize = true;
            this.checkBox_override.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_override.Location = new System.Drawing.Point(314, 10);
            this.checkBox_override.Name = "checkBox_override";
            this.checkBox_override.Size = new System.Drawing.Size(144, 34);
            this.checkBox_override.TabIndex = 15;
            this.checkBox_override.Text = "覆盖原文件";
            this.checkBox_override.CheckedChanged += new System.EventHandler(this.checkBox_override_CheckedChanged);
            // 
            // checkBox_sameOutput
            // 
            this.checkBox_sameOutput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox_sameOutput.AutoSize = true;
            this.checkBox_sameOutput.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_sameOutput.Location = new System.Drawing.Point(464, 10);
            this.checkBox_sameOutput.Name = "checkBox_sameOutput";
            this.checkBox_sameOutput.Size = new System.Drawing.Size(207, 34);
            this.checkBox_sameOutput.TabIndex = 16;
            this.checkBox_sameOutput.Text = "输出到相同文件夹";
            this.checkBox_sameOutput.CheckedChanged += new System.EventHandler(this.checkBox_sameOutput_CheckedChanged);
            // 
            // checkBox_openOutput
            // 
            this.checkBox_openOutput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox_openOutput.AutoSize = true;
            this.checkBox_openOutput.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_openOutput.Location = new System.Drawing.Point(677, 10);
            this.checkBox_openOutput.Name = "checkBox_openOutput";
            this.checkBox_openOutput.Size = new System.Drawing.Size(249, 34);
            this.checkBox_openOutput.TabIndex = 17;
            this.checkBox_openOutput.Text = "转换成功后打开文件夹";
            // 
            // button_convert
            // 
            this.button_convert.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_convert.AutoSize = true;
            this.button_convert.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button_convert.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_convert.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_convert.Location = new System.Drawing.Point(956, 3);
            this.button_convert.Name = "button_convert";
            this.button_convert.Size = new System.Drawing.Size(124, 48);
            this.button_convert.TabIndex = 18;
            this.button_convert.Text = "转换";
            this.button_convert.Click += new System.EventHandler(this.button_convert_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1121, 420);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "GBK 转 UTF-8";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label_file;
        private Label label_directory;
        private Label label_output;
        private TextBox textBox_file;
        private TextBox textBox_directory;
        private TextBox textBox_output;
        private Button button_file;
        private Button button_chooseDirectory;
        private Button button_chooseOutput;
        private Button button_openDirectory;
        private Button button_openOutput;
        private CheckBox checkBox_bom;
        private CheckBox checkBox_recur;
        private CheckBox checkBox_override;
        private CheckBox checkBox_sameOutput;
        private CheckBox checkBox_openOutput;
        private Button button_convert;
    }
}