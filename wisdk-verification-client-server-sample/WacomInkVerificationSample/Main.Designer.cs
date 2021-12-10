namespace WacomInkVerificationSample
{
    partial class Main
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
            this.lstTemplates = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReset = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSignature = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdReadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdVerify = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.imgSignature = new System.Windows.Forms.PictureBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSignature)).BeginInit();
            this.SuspendLayout();
            // 
            // lstTemplates
            // 
            this.lstTemplates.FormattingEnabled = true;
            this.lstTemplates.Location = new System.Drawing.Point(12, 27);
            this.lstTemplates.Name = "lstTemplates";
            this.lstTemplates.Size = new System.Drawing.Size(153, 199);
            this.lstTemplates.TabIndex = 1;
            this.lstTemplates.SelectedIndexChanged += new System.EventHandler(this.OnTemplateSelectionChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(172, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.OnAddClick);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(171, 56);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.OnResetClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(172, 86);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.OnDeleteClick);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTemplate,
            this.mnuSignature,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(601, 24);
            this.mnuMain.TabIndex = 6;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuTemplate
            // 
            this.mnuTemplate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdAdd,
            this.cmdReset,
            this.cmdDelete,
            this.cmdStatus,
            this.toolStripSeparator2,
            this.cmdOptions,
            this.toolStripSeparator1,
            this.cmdExit});
            this.mnuTemplate.Name = "mnuTemplate";
            this.mnuTemplate.Size = new System.Drawing.Size(67, 20);
            this.mnuTemplate.Text = "Template";
            this.mnuTemplate.DropDownOpened += new System.EventHandler(this.OnTemplateMenuOpened);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(125, 22);
            this.cmdAdd.Text = "Add...";
            this.cmdAdd.Click += new System.EventHandler(this.OnAddClick);
            // 
            // cmdReset
            // 
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(125, 22);
            this.cmdReset.Text = "Reset";
            this.cmdReset.Click += new System.EventHandler(this.OnResetClick);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(125, 22);
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.Click += new System.EventHandler(this.OnDeleteClick);
            // 
            // cmdStatus
            // 
            this.cmdStatus.Name = "cmdStatus";
            this.cmdStatus.Size = new System.Drawing.Size(125, 22);
            this.cmdStatus.Text = "Status...";
            this.cmdStatus.Click += new System.EventHandler(this.OnStatusClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(122, 6);
            // 
            // cmdOptions
            // 
            this.cmdOptions.Name = "cmdOptions";
            this.cmdOptions.Size = new System.Drawing.Size(125, 22);
            this.cmdOptions.Text = "Options...";
            this.cmdOptions.Click += new System.EventHandler(this.OnOptionsClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // cmdExit
            // 
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(125, 22);
            this.cmdExit.Text = "Exit";
            this.cmdExit.Click += new System.EventHandler(this.OnExitClick);
            // 
            // mnuSignature
            // 
            this.mnuSignature.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdReadFile,
            this.cmdCapture,
            this.cmdVerify});
            this.mnuSignature.Name = "mnuSignature";
            this.mnuSignature.Size = new System.Drawing.Size(69, 20);
            this.mnuSignature.Text = "Signature";
            this.mnuSignature.DropDownOpened += new System.EventHandler(this.OnSignatureMenuOpened);
            // 
            // cmdReadFile
            // 
            this.cmdReadFile.Name = "cmdReadFile";
            this.cmdReadFile.Size = new System.Drawing.Size(180, 22);
            this.cmdReadFile.Text = "Read file...";
            this.cmdReadFile.Click += new System.EventHandler(this.OnReadClick);
            // 
            // cmdCapture
            // 
            this.cmdCapture.Name = "cmdCapture";
            this.cmdCapture.Size = new System.Drawing.Size(180, 22);
            this.cmdCapture.Text = "Capture...";
            this.cmdCapture.Click += new System.EventHandler(this.OnCaptureClick);
            // 
            // cmdVerify
            // 
            this.cmdVerify.Name = "cmdVerify";
            this.cmdVerify.Size = new System.Drawing.Size(180, 22);
            this.cmdVerify.Text = "Verify";
            this.cmdVerify.Click += new System.EventHandler(this.OnVerifyClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // cmdAbout
            // 
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Size = new System.Drawing.Size(107, 22);
            this.cmdAbout.Text = "About";
            this.cmdAbout.Click += new System.EventHandler(this.OnAboutClick);
            // 
            // imgSignature
            // 
            this.imgSignature.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgSignature.Location = new System.Drawing.Point(275, 27);
            this.imgSignature.Name = "imgSignature";
            this.imgSignature.Size = new System.Drawing.Size(312, 173);
            this.imgSignature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgSignature.TabIndex = 7;
            this.imgSignature.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(381, 206);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(100, 23);
            this.btnCapture.TabIndex = 8;
            this.btnCapture.Text = "Capture...";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.OnCaptureClick);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(275, 206);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(100, 23);
            this.btnRead.TabIndex = 9;
            this.btnRead.Text = "Read...";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.OnReadClick);
            // 
            // btnVerify
            // 
            this.btnVerify.Enabled = false;
            this.btnVerify.Location = new System.Drawing.Point(487, 206);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(100, 23);
            this.btnVerify.TabIndex = 10;
            this.btnVerify.Text = "Verify...";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.OnVerifyClick);
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 244);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.imgSignature);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstTemplates);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "Main";
            this.Text = "Wacom Ink SDK for Verification";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSignature)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lstTemplates;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplate;
        private System.Windows.Forms.ToolStripMenuItem cmdOptions;
        private System.Windows.Forms.ToolStripMenuItem cmdExit;
        private System.Windows.Forms.ToolStripMenuItem cmdAdd;
        private System.Windows.Forms.ToolStripMenuItem cmdReset;
        private System.Windows.Forms.ToolStripMenuItem cmdDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuSignature;
        private System.Windows.Forms.ToolStripMenuItem cmdReadFile;
        private System.Windows.Forms.ToolStripMenuItem cmdCapture;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem cmdAbout;
        private System.Windows.Forms.PictureBox imgSignature;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.ToolStripMenuItem cmdStatus;
        private System.Windows.Forms.ToolStripMenuItem cmdVerify;
        private System.Windows.Forms.Button btnVerify;
    }
}

