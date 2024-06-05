/*  OptionsDlg.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WacomVerificationSample
{
    public partial class OptionsDlg : Form
    {

        /// <summary>
        /// User interface for setting application options
        /// </summary>
        public OptionsDlg()
        {
            InitializeComponent();

            var options = Options.Instance;

            txtFolder.Text = options.TemplateFolder;

            numTemplateSize.Value = options.ConfigurationOptions.TemplateSize;
            numEnrollScore.Value = (decimal)options.ConfigurationOptions.EnrollmentScore;
            numUpdateInterval.Value = options.ConfigurationOptions.UpdateInterval;

            var sigStyles = new Dictionary<string, WacomVerification.SignatureStyle>()
            {
                { "Cursive", WacomVerification.SignatureStyle.Cursive },
                { "Kanji"  , WacomVerification.SignatureStyle.Kanji }
            };

            cbxSigStyle.DataSource = new BindingSource(sigStyles, null);
            cbxSigStyle.DisplayMember = "Key";
            cbxSigStyle.ValueMember = "Value";
            cbxSigStyle.SelectedValue = options.ConfigurationOptions.SignatureStyle;

            chkRemoveSpeckle.Checked = options.ImageOptions.RemoveSpeckle;
            chkRemoveFold.Checked = options.ImageOptions.RemoveFold;
            chkRemoveBox.Checked = options.ImageOptions.RemoveBox;
            numMinLineLength.Value = (decimal)options.ImageOptions.MinSigningLineLength;
            numMaxLineThickness.Value = (decimal)options.ImageOptions.MaxSigningLineThickness;

            chkAdjustContrast.Checked = options.ImageOptions.AdjustContrast;
            numContrast.Value = options.ImageOptions.Contrast;

            numResolution.Value = (decimal)options.ImageOptions.ImageResolution;
            chkSetImageResolution.Checked = options.ImageOptions.SetImageResolution;

        }

        private void UpdateOptions()
        {
            var options = Options.Instance;

            if (options.TemplateFolder != txtFolder.Text)
            {
                // Ensure new folder exists
                Directory.CreateDirectory(txtFolder.Text);

                // Move existing template files
                if (Directory.Exists(Options.Instance.TemplateFolder))
                {
                    foreach (var file in Directory.EnumerateFiles(Options.Instance.TemplateFolder, $"*.{Template.Extn}"))
                    {
                        string dstn = Path.Combine(txtFolder.Text, Path.GetFileName(file));
                        File.Move(file, dstn);
                    }
                    options.TemplateFolder = txtFolder.Text;
                }
            }

            options.ConfigurationOptions.TemplateSize = (ushort)numTemplateSize.Value;
            options.ConfigurationOptions.EnrollmentScore = (float)numEnrollScore.Value;
            options.ConfigurationOptions.UpdateInterval = (ushort)numUpdateInterval.Value;
            options.ConfigurationOptions.SignatureStyle = (WacomVerification.SignatureStyle)cbxSigStyle.SelectedValue;

            options.ImageOptions.RemoveSpeckle = chkRemoveSpeckle.Checked;
            options.ImageOptions.RemoveFold = chkRemoveFold.Checked;
            options.ImageOptions.RemoveBox = chkRemoveBox.Checked;
            options.ImageOptions.MinSigningLineLength = (float)numMinLineLength.Value;
            options.ImageOptions.MaxSigningLineThickness = (float)numMaxLineThickness.Value;

            options.ImageOptions.AdjustContrast = chkAdjustContrast.Checked;
            options.ImageOptions.Contrast = (short)numContrast.Value;

            options.ImageOptions.ImageResolution = (ushort)numResolution.Value;
            options.ImageOptions.SetImageResolution = chkSetImageResolution.Checked;

            options.Save();
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            UpdateOptions();
            Close();
        }

        private void OnFolderClick(object sender, EventArgs e)
        {
            var folderDlg = new FolderBrowserDialog()
            {
                SelectedPath = Options.Instance.TemplateFolder
            };
            if (folderDlg.ShowDialog(this) == DialogResult.OK)
            {
                Options.Instance.TemplateFolder = folderDlg.SelectedPath;
            }
        }
    }
}
