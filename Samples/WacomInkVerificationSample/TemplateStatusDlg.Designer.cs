namespace WacomVerificationSample
{
    partial class TemplateStatusDlg
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
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNumSigsDyn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEnrollStateDyn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEnrollSizeDyn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNumSigsStat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEnrollStateStat = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEnrollSizeStat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(236, 13);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNumSigsDyn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEnrollStateDyn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEnrollSizeDyn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 103);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dynamic Enrollment Status";
            // 
            // txtNumSigsDyn
            // 
            this.txtNumSigsDyn.Location = new System.Drawing.Point(102, 72);
            this.txtNumSigsDyn.Name = "txtNumSigsDyn";
            this.txtNumSigsDyn.ReadOnly = true;
            this.txtNumSigsDyn.Size = new System.Drawing.Size(76, 20);
            this.txtNumSigsDyn.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Num signatures:";
            // 
            // txtEnrollStateDyn
            // 
            this.txtEnrollStateDyn.Location = new System.Drawing.Point(102, 46);
            this.txtEnrollStateDyn.Name = "txtEnrollStateDyn";
            this.txtEnrollStateDyn.ReadOnly = true;
            this.txtEnrollStateDyn.Size = new System.Drawing.Size(76, 20);
            this.txtEnrollStateDyn.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enrollment state:";
            // 
            // txtEnrollSizeDyn
            // 
            this.txtEnrollSizeDyn.Location = new System.Drawing.Point(102, 20);
            this.txtEnrollSizeDyn.Name = "txtEnrollSizeDyn";
            this.txtEnrollSizeDyn.ReadOnly = true;
            this.txtEnrollSizeDyn.Size = new System.Drawing.Size(76, 20);
            this.txtEnrollSizeDyn.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enrollment size:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNumSigsStat);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtEnrollStateStat);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtEnrollSizeStat);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(13, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 103);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Static Enrollment Status";
            // 
            // txtNumSigsStat
            // 
            this.txtNumSigsStat.Location = new System.Drawing.Point(102, 72);
            this.txtNumSigsStat.Name = "txtNumSigsStat";
            this.txtNumSigsStat.ReadOnly = true;
            this.txtNumSigsStat.Size = new System.Drawing.Size(76, 20);
            this.txtNumSigsStat.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Num signatures:";
            // 
            // txtEnrollStateStat
            // 
            this.txtEnrollStateStat.Location = new System.Drawing.Point(102, 46);
            this.txtEnrollStateStat.Name = "txtEnrollStateStat";
            this.txtEnrollStateStat.ReadOnly = true;
            this.txtEnrollStateStat.Size = new System.Drawing.Size(76, 20);
            this.txtEnrollStateStat.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Enrollment state:";
            // 
            // txtEnrollSizeStat
            // 
            this.txtEnrollSizeStat.Location = new System.Drawing.Point(102, 20);
            this.txtEnrollSizeStat.Name = "txtEnrollSizeStat";
            this.txtEnrollSizeStat.ReadOnly = true;
            this.txtEnrollSizeStat.Size = new System.Drawing.Size(76, 20);
            this.txtEnrollSizeStat.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Enrollment size:";
            // 
            // StatusDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(327, 234);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatusDlg";
            this.ShowInTaskbar = false;
            this.Text = "Template Status";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNumSigsDyn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEnrollStateDyn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEnrollSizeDyn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNumSigsStat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEnrollStateStat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEnrollSizeStat;
        private System.Windows.Forms.Label label6;
    }
}