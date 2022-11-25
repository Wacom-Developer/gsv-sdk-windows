/*  Main.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using WacomVerification;
using FlSigCaptLib;
using FLSIGCTLLib;

namespace WacomVerificationSample
{
    /// <summary>
    /// The main form for the application
    /// </summary>
    public partial class Main : Form
    {
        #region Fields
        
        private object mSignature = null;  // SigObj or filename
        private const string mLicense = "<<insert license here>>";
        
        #endregion

        #region Constructor
        public Main()
        {
            InitializeComponent();

            /// Load saved templates
            if (Directory.Exists(Options.Instance.TemplateFolder))
            {
                foreach (var file in Directory.EnumerateFiles(Options.Instance.TemplateFolder, $"*{Template.Extn}"))
                {
                    lstTemplates.Items.Add(Template.LoadFromFile(file));
                }
            }
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Display About dialog
        /// </summary>
        private void OnAboutClick(object sender, EventArgs e)
        {
            AboutDlg aboutBox = new AboutDlg(IsLicensed);
            aboutBox.ShowDialog(this);
        }

        /// <summary>
        /// Close the form and terminate the application
        /// </summary>
        private void OnExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Add a new template
        /// </summary>
        private void OnAddClick(object sender, EventArgs e)
        {
            NewTemplateDlg newTemplateDlg = new NewTemplateDlg();
            if (newTemplateDlg.ShowDialog(this) == DialogResult.OK)
            {
                if (lstTemplates.Items.Contains(newTemplateDlg.TemplateName))
                {
                    MessageBox.Show($"A template named '{newTemplateDlg.TemplateName}' already exists", "Add Template",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Template template = new Template(newTemplateDlg.TemplateName);
                    int idx = lstTemplates.Items.Add(template);
                    lstTemplates.SelectedIndex = idx;
                }
            }
        }

        /// <summary>
        /// Reset the currently selected template
        /// </summary>
        /// <remarks>Clears and reinitializes the template with the current options</remarks>
        private void OnResetClick(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedIndex >= 0)
            {
                Template template = (Template)lstTemplates.SelectedItem;
                if (MessageBox.Show($"Reset template '{template.Name}'", "Reset Template", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    template.Reset();  
                }
            }
        }

        /// <summary>
        /// Deletes the currently selected template
        /// </summary>
        private void OnDeleteClick(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedIndex >= 0)
            {
                Template template = (Template)lstTemplates.SelectedItem;
                if (MessageBox.Show($"Delete template '{template.Name}'", "Delete Template", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    template.Delete();
                    lstTemplates.Items.RemoveAt(lstTemplates.SelectedIndex);
                }
            }        
        }

        /// <summary>
        /// Displays the Options dialog for setting template options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOptionsClick(object sender, EventArgs e)
        {
            OptionsDlg optionsDlg = new OptionsDlg();
            optionsDlg.ShowDialog(this);
        }

        /// <summary>
        /// Displays the Status dialog showing details of the currently selected template
        /// </summary>
        private void OnStatusClick(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedIndex >= 0)
            {
                SignatureEngine sigEngine = new SignatureEngine();
                Template template = (Template)lstTemplates.SelectedItem;
                var status = sigEngine.GetTemplateStatus(template.Data);

                var statusDlg = new TemplateStatusDlg(status);
                statusDlg.ShowDialog(this);
            }
        }

        /// <summary>
        /// Captures a dynamic signature for subsequent enrollment or verification
        /// </summary>
        /// <remarks>Uses components from the Wacom Signaure SDK</remarks>
        private void OnCaptureClick(object sender, EventArgs e)
        {
            if (!CheckLicense())
                return;
            try
            {
                ClearSignature();

                var capture = new DynamicCaptureClass();
                var sigCtl = new SigCtlClass();

                capture.Licence = mLicense;
                var captResult = capture.Capture(sigCtl, Environment.UserName, "Verification test");
                if (captResult == DynamicCaptureResult.DynCaptOK)
                {
                    ISigObj4 sigObj = sigCtl.Signature as ISigObj4;

                    SetSignature(sigObj);
                }
                else if (captResult != DynamicCaptureResult.DynCaptCancel)
                {
                    MessageBox.Show($"Capture failed.\nError: {captResult}", "Capture Signature",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                CheckButtonsEnabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}", null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Loads dynamic signature data or a signature image from a file 
        /// </summary>
        private void OnReadClick(object sender, EventArgs e)
        {
            ClearSignature();

            try
            {
                var openFileDlg = new OpenFileDialog()
                {
                    Filter =
                        "All signature files (*.txt;*.fss;*.png;*.tif;*.jpg;*.bmp;*.gif)|*.txt;*.fss;*.png;*.tif;*.jpg;*.bmp;*.gif|" +
                        "Signature text files (*.txt)|*.txt|" +
                        "Signature data files (*.fss)|*.fss|" +
                        "Image files (*.png;*.tif;*.jpg;*.bmp;*.gif)|*.png;*.tif;*.jpg;*.bmp;*.gif|" +
                        "All Files (*.*)|*.*"
                };

                if (openFileDlg.ShowDialog(this) == DialogResult.OK)
                {
                    SetSignature(openFileDlg.FileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file:\n{ex.Message}",
                    "Read",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            finally
            {
                CheckButtonsEnabled();
            }
        }

        /// <summary>
        /// Verifies the current signature using the currently selected template
        /// </summary>
        private void OnVerifyClick(object sender, EventArgs e)
        {
            VerifySignature();
        }

        /// <summary>
        /// Event handler for change of selection in list of templates 
        /// </summary>
        private void OnTemplateSelectionChanged(object sender, EventArgs e)
        {
            CheckButtonsEnabled();
        }

        /// <summary>
        /// Event handler for opening of Template menu
        /// </summary>
        /// <remarks>Enables or disables commands depending on whether a template is currently selected</remarks>
        private void OnTemplateMenuOpened(object sender, EventArgs e)
        {
            bool tpltSelected = lstTemplates.SelectedIndex >= 0;
            cmdReset.Enabled = tpltSelected;
            cmdDelete.Enabled = tpltSelected;
            cmdStatus.Enabled = tpltSelected;
        }

        /// <summary>
        /// Event handler for opening of Signature menu
        /// </summary>
        /// <remarks>Enables or disables commands depending on whether there is currently a signature</remarks>
        private void OnSignatureMenuOpened(object sender, EventArgs e)
        {
            bool haveSig = mSignature != null;
            bool tpltSelected = lstTemplates.SelectedIndex >= 0;
            cmdVerify.Enabled = haveSig && tpltSelected;
        }

        #endregion

        #region Signature Handling

        /// <summary>
        /// Sets the signature from a captured signature object
        /// </summary>
        /// <param name="sigObj">SigObj containing dynamic signature data</param>
        /// <remarks>Generates an image of the signature to display</remarks>
        private void SetSignature(ISigObj4 sigObj)
        {
            stdole.IPictureDisp pic = (stdole.IPictureDisp)sigObj.RenderBitmap(null,
                                                          imgSignature.Width, imgSignature.Height,
                                                          "image/bmp",
                                                          0.5f,           // ink width
                                                          0x0, 0xFFFFFF,  // black on white
                                                          5, 5,           // padding
                                                          RBFlags.RenderOutputPicture | RBFlags.RenderColor24BPP);

            IntPtr paletteHandle = new IntPtr(pic.hPal);
            IntPtr bitmapHandle = new IntPtr(pic.Handle);

            imgSignature.Image = Image.FromHbitmap(bitmapHandle, paletteHandle);
            mSignature = sigObj;
        }

        /// <summary>
        /// Sets the signature from a file
        /// </summary>
        /// <param name="fileName">Name of file containing signature data or a signature image</param>
        /// <remarks>For images, creates a scaled copy of the image to display but the original
        ///     is always used for ernollment and verification<br/>
        ///     With throw exceptions if file doesn't exist or isn't valid signature data or an image
        /// </remarks>
        private void SetSignature(string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".txt":
                    LoadSigText(fileName);
                    break;
                case ".fss":
                    LoadSigData(fileName);
                    break;
                default:    // assume image file
                    var img = Image.FromFile(fileName);
                    if (img.Width > imgSignature.Width || img.Height > imgSignature.Height)
                    {
                        float scale = Math.Min((float)imgSignature.Width / img.Width, (float)imgSignature.Height / img.Height);
                        img = new Bitmap(img, (int)(scale * img.Width), (int)(scale * img.Height));
                    }
                    imgSignature.Image = img;
                    mSignature = fileName;
                    break;
            }
        }

        /// <summary>
        /// Verifies the current signature using the currently selected template
        /// </summary>
        private void VerifySignature()
        {
            if (!CheckLicense())
                return;

            try
            {
                if (mSignature != null && lstTemplates.SelectedIndex >= 0)
                {
                    var sigEngine = new SignatureEngine();
                    var template = (Template)lstTemplates.SelectedItem;
                    sigEngine.License = mLicense;

                    var result = sigEngine.VerifySignature(template.Data, mSignature);
                    template.Data = result.UpdatedTemplate;

                    var resultsDlg = new ResultsDlg(result);
                    resultsDlg.ShowDialog(this);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}", null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Loads dynamic signature data from a text file
        /// </summary>
        /// <param name="filename">Name of file containing base-64 encoded FSS data</param>
        private void LoadSigText(string filename)
        {
            ISigObj4 sigObj = new SigObjClass
            {
                SigText = File.ReadAllText(filename)
            };
            SetSignature(sigObj);
        }

        /// <summary>
        /// Loads dynamic signature data from a binary file
        /// </summary>
        /// <param name="filename">Name of file containing binary FSS data</param>
        private void LoadSigData(string filename)
        {
            ISigObj4 sigObj = new SigObjClass
            {
                SigData = File.ReadAllBytes(filename)
            };
            SetSignature(sigObj);
        }

        #endregion

        #region Drag & Drop

        /// <summary>
        /// Event handler for drag over
        /// </summary>
        private void OnDragOver(object sender, DragEventArgs e)
        {
            DragDropEffects effect = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                object[] files = (object[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    effect = DragDropEffects.Copy;
                }
            }
            e.Effect = effect;
        }

        /// <summary>
        /// Event handler for drop
        /// </summary>
        private void OnDragDrop(object sender, DragEventArgs e)
        {
            string filename = null;

            ClearSignature();
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    object[] files = (object[])e.Data.GetData(DataFormats.FileDrop);

                    if (files.Length == 1)
                    {
                        filename = (string)files[0];
                        SetSignature(filename);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file '{filename}':\n{ex.Message}", 
                    "Drag and Drop", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation);
            }
            finally 
            {
                CheckButtonsEnabled();
            }
            
        }

        #endregion

        #region Support Methods

        /// <summary>
        /// True if license string (mLicense) is valid
        /// </summary>
        /// <remarks>
        /// Only checks if the license string is valid. 
        /// Does not check for license expired or whether it allows verification
        /// </remarks>
        private static bool IsLicensed
        {
            get
            {
                var license = new WacomLicenceLib.LicenceClass();

                if (license.SetLicence(mLicense).Result == 0
                    && license.Check(110).Result == (int)WacomLicenceLib.CheckStatus.Allowed)
                {
                    return true;
                }
                return false;

            }
        }

        /// <summary>
        /// Resets signarture and image
        /// </summary>
        private void ClearSignature()
        {
            mSignature = null;
            imgSignature.Image = null;
        }

        /// <summary>
        /// Enables and disables buttons depending on whether there is currently a signature and a template selected
        /// </summary>
        private void CheckButtonsEnabled()
        {
            bool haveSig = mSignature != null;
            bool tpltSelected = lstTemplates.SelectedIndex >= 0;

            btnReset.Enabled = tpltSelected;
            btnDelete.Enabled = tpltSelected;

            btnVerify.Enabled = haveSig && tpltSelected;
        }

        /// <summary>
        /// Checks if appp is licensed; displays error message if not
        /// </summary>
        private bool CheckLicense()
        {
            if (!IsLicensed)
            {
                MessageBox.Show("Use of this sample application requires a valid, current license string from Wacom",
                                null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
