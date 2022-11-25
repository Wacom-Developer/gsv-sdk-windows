/*  OptionsDlg.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;

using GsvClient;
using GsvClient.Models;

namespace WacomInkVerificationSample
{
    public partial class OptionsDlg : Form
    {
        IGsvClient _gsvClient;

        private async Task<TemplateOptions> InitAsync()
        {
            var response = await _gsvClient.GetClientTemplateOptionsAsync(new GetClientTemplateOptionsRequest());
            return response.Options;
        }
        
        /// <summary>
        /// User interface for setting application options
        /// </summary>
        public OptionsDlg(IGsvClient gsvClient)
        {
            InitializeComponent();

            _gsvClient = gsvClient;

            var task = Task<TemplateOptions>.Run(async () => await InitAsync());
            task.Wait();
            var options = task.Result;
                        
            numTemplateSize.Value = options.ConfigurationOptions.TemplateSize;
            numEnrollScore.Value = (decimal)options.ConfigurationOptions.EnrollmentScore;
            numUpdateInterval.Value = options.ConfigurationOptions.UpdateInterval;

            var sigStyles = new Dictionary<string, SignatureStyle>()
            {
                { "Cursive", SignatureStyle.Cursive },
                { "Kanji"  , SignatureStyle.Kanji },
                { "Auto Detect", SignatureStyle.AutoDetect }
            };

            cbxSigStyle.DataSource = new BindingSource(sigStyles, null);
            cbxSigStyle.DisplayMember = "Key";
            cbxSigStyle.ValueMember = "Value";
            cbxSigStyle.SelectedValue = options.ConfigurationOptions.SignatureStyle;

            chkIgnoreDateTime.Checked = options.ConfigurationOptions.IgnoreDateTime;
            chkForceEnroll.Checked = options.ConfigurationOptions.ForceEnrollment;

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

        private async Task UpdateAsync(TemplateOptions templateOptions)
        {
            var request = new UpdateClientTemplateOptionsRequest()
            {
                Options = templateOptions
            };
            await _gsvClient.UpdateClientTemplateOptionsAsync(request);
        }

        private void UpdateOptions()
        {
            var options = new TemplateOptions();
            
            options.ConfigurationOptions.TemplateSize = (ushort)numTemplateSize.Value;
            options.ConfigurationOptions.EnrollmentScore = (float)numEnrollScore.Value;
            options.ConfigurationOptions.UpdateInterval = (ushort)numUpdateInterval.Value;
            options.ConfigurationOptions.SignatureStyle = (SignatureStyle)cbxSigStyle.SelectedValue;
            options.ConfigurationOptions.IgnoreDateTime = chkIgnoreDateTime.Checked;
            options.ConfigurationOptions.ForceEnrollment = chkForceEnroll.Checked;

            options.ImageOptions.RemoveSpeckle = chkRemoveSpeckle.Checked;
            options.ImageOptions.RemoveFold = chkRemoveFold.Checked;
            options.ImageOptions.RemoveBox = chkRemoveBox.Checked;
            options.ImageOptions.MinSigningLineLength = (float)numMinLineLength.Value;
            options.ImageOptions.MaxSigningLineThickness = (float)numMaxLineThickness.Value;

            options.ImageOptions.AdjustContrast = chkAdjustContrast.Checked;
            options.ImageOptions.Contrast = (short)numContrast.Value;

            options.ImageOptions.ImageResolution = (ushort)numResolution.Value;
            options.ImageOptions.SetImageResolution = chkSetImageResolution.Checked;

            var task = Task.Run(async () => await UpdateAsync(options));
            task.Wait();
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            UpdateOptions();
            Close();
        }       
    }
}
