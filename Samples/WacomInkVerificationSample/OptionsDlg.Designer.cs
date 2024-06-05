namespace WacomVerificationSample
{
    partial class OptionsDlg
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxSigStyle = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numUpdateInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numEnrollScore = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numTemplateSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numResolution = new System.Windows.Forms.NumericUpDown();
            this.chkSetImageResolution = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numContrast = new System.Windows.Forms.NumericUpDown();
            this.chkAdjustContrast = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numMaxLineThickness = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numMinLineLength = new System.Windows.Forms.NumericUpDown();
            this.chkRemoveLine = new System.Windows.Forms.CheckBox();
            this.chkRemoveBox = new System.Windows.Forms.CheckBox();
            this.chkRemoveFold = new System.Windows.Forms.CheckBox();
            this.chkRemoveSpeckle = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnrollScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTemplateSize)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numResolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxLineThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinLineLength)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxSigStyle);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numUpdateInterval);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numEnrollScore);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numTemplateSize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // cbxSigStyle
            // 
            this.cbxSigStyle.FormattingEnabled = true;
            this.cbxSigStyle.Location = new System.Drawing.Point(102, 100);
            this.cbxSigStyle.Name = "cbxSigStyle";
            this.cbxSigStyle.Size = new System.Drawing.Size(61, 21);
            this.cbxSigStyle.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Signature Style:";
            // 
            // numUpdateInterval
            // 
            this.numUpdateInterval.Location = new System.Drawing.Point(102, 73);
            this.numUpdateInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numUpdateInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpdateInterval.Name = "numUpdateInterval";
            this.numUpdateInterval.Size = new System.Drawing.Size(61, 20);
            this.numUpdateInterval.TabIndex = 6;
            this.numUpdateInterval.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Update interval:";
            // 
            // numEnrollScore
            // 
            this.numEnrollScore.DecimalPlaces = 2;
            this.numEnrollScore.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numEnrollScore.Location = new System.Drawing.Point(102, 47);
            this.numEnrollScore.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEnrollScore.Name = "numEnrollScore";
            this.numEnrollScore.Size = new System.Drawing.Size(61, 20);
            this.numEnrollScore.TabIndex = 4;
            this.numEnrollScore.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enrollment score:";
            // 
            // numTemplateSize
            // 
            this.numTemplateSize.Location = new System.Drawing.Point(102, 21);
            this.numTemplateSize.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numTemplateSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numTemplateSize.Name = "numTemplateSize";
            this.numTemplateSize.Size = new System.Drawing.Size(61, 20);
            this.numTemplateSize.TabIndex = 2;
            this.numTemplateSize.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Template size:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.numResolution);
            this.groupBox3.Controls.Add(this.chkSetImageResolution);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numContrast);
            this.groupBox3.Controls.Add(this.chkAdjustContrast);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.numMaxLineThickness);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numMinLineLength);
            this.groupBox3.Controls.Add(this.chkRemoveLine);
            this.groupBox3.Controls.Add(this.chkRemoveBox);
            this.groupBox3.Controls.Add(this.chkRemoveFold);
            this.groupBox3.Controls.Add(this.chkRemoveSpeckle);
            this.groupBox3.Location = new System.Drawing.Point(13, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(304, 196);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Image";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(154, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Image resolution";
            // 
            // numResolution
            // 
            this.numResolution.Location = new System.Drawing.Point(244, 163);
            this.numResolution.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numResolution.Name = "numResolution";
            this.numResolution.Size = new System.Drawing.Size(50, 20);
            this.numResolution.TabIndex = 12;
            // 
            // chkSetImageResolution
            // 
            this.chkSetImageResolution.AutoSize = true;
            this.chkSetImageResolution.Location = new System.Drawing.Point(138, 140);
            this.chkSetImageResolution.Name = "chkSetImageResolution";
            this.chkSetImageResolution.Size = new System.Drawing.Size(121, 17);
            this.chkSetImageResolution.TabIndex = 11;
            this.chkSetImageResolution.Text = "Set image resolution";
            this.chkSetImageResolution.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(154, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Contrast";
            // 
            // numContrast
            // 
            this.numContrast.Location = new System.Drawing.Point(244, 114);
            this.numContrast.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numContrast.Name = "numContrast";
            this.numContrast.Size = new System.Drawing.Size(50, 20);
            this.numContrast.TabIndex = 9;
            // 
            // chkAdjustContrast
            // 
            this.chkAdjustContrast.AutoSize = true;
            this.chkAdjustContrast.Location = new System.Drawing.Point(138, 91);
            this.chkAdjustContrast.Name = "chkAdjustContrast";
            this.chkAdjustContrast.Size = new System.Drawing.Size(96, 17);
            this.chkAdjustContrast.TabIndex = 8;
            this.chkAdjustContrast.Text = "Adjust contrast";
            this.chkAdjustContrast.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(154, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Max thickness";
            // 
            // numMaxLineThickness
            // 
            this.numMaxLineThickness.DecimalPlaces = 2;
            this.numMaxLineThickness.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numMaxLineThickness.Location = new System.Drawing.Point(244, 67);
            this.numMaxLineThickness.Name = "numMaxLineThickness";
            this.numMaxLineThickness.Size = new System.Drawing.Size(50, 20);
            this.numMaxLineThickness.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(154, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Min length";
            // 
            // numMinLineLength
            // 
            this.numMinLineLength.DecimalPlaces = 2;
            this.numMinLineLength.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numMinLineLength.Location = new System.Drawing.Point(244, 43);
            this.numMinLineLength.Name = "numMinLineLength";
            this.numMinLineLength.Size = new System.Drawing.Size(50, 20);
            this.numMinLineLength.TabIndex = 4;
            // 
            // chkRemoveLine
            // 
            this.chkRemoveLine.AutoSize = true;
            this.chkRemoveLine.Location = new System.Drawing.Point(138, 20);
            this.chkRemoveLine.Name = "chkRemoveLine";
            this.chkRemoveLine.Size = new System.Drawing.Size(121, 17);
            this.chkRemoveLine.TabIndex = 3;
            this.chkRemoveLine.Text = "Remove signing line";
            this.chkRemoveLine.UseVisualStyleBackColor = true;
            // 
            // chkRemoveBox
            // 
            this.chkRemoveBox.AutoSize = true;
            this.chkRemoveBox.Location = new System.Drawing.Point(7, 68);
            this.chkRemoveBox.Name = "chkRemoveBox";
            this.chkRemoveBox.Size = new System.Drawing.Size(86, 17);
            this.chkRemoveBox.TabIndex = 2;
            this.chkRemoveBox.Text = "Remove box";
            this.chkRemoveBox.UseVisualStyleBackColor = true;
            // 
            // chkRemoveFold
            // 
            this.chkRemoveFold.AutoSize = true;
            this.chkRemoveFold.Location = new System.Drawing.Point(7, 44);
            this.chkRemoveFold.Name = "chkRemoveFold";
            this.chkRemoveFold.Size = new System.Drawing.Size(86, 17);
            this.chkRemoveFold.TabIndex = 1;
            this.chkRemoveFold.Text = "Remove fold";
            this.chkRemoveFold.UseVisualStyleBackColor = true;
            // 
            // chkRemoveSpeckle
            // 
            this.chkRemoveSpeckle.AutoSize = true;
            this.chkRemoveSpeckle.Location = new System.Drawing.Point(7, 20);
            this.chkRemoveSpeckle.Name = "chkRemoveSpeckle";
            this.chkRemoveSpeckle.Size = new System.Drawing.Size(106, 17);
            this.chkRemoveSpeckle.TabIndex = 0;
            this.chkRemoveSpeckle.Text = "Remove speckle";
            this.chkRemoveSpeckle.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnFolder);
            this.groupBox4.Controls.Add(this.txtFolder);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(305, 52);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Template folder";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(267, 20);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(32, 20);
            this.btnFolder.TabIndex = 1;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.OnFolderClick);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(8, 20);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(252, 20);
            this.txtFolder.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(324, 13);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OnOKClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(324, 43);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // OptionsDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(413, 411);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDlg";
            this.ShowInTaskbar = false;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnrollScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTemplateSize)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numResolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxLineThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinLineLength)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numEnrollScore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTemplateSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numUpdateInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numResolution;
        private System.Windows.Forms.CheckBox chkSetImageResolution;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numContrast;
        private System.Windows.Forms.CheckBox chkAdjustContrast;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numMaxLineThickness;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numMinLineLength;
        private System.Windows.Forms.CheckBox chkRemoveLine;
        private System.Windows.Forms.CheckBox chkRemoveBox;
        private System.Windows.Forms.CheckBox chkRemoveFold;
        private System.Windows.Forms.CheckBox chkRemoveSpeckle;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbxSigStyle;
        private System.Windows.Forms.Label label8;
    }
}