/*  Main.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

using FlSigCaptLib;
using FLSIGCTLLib;

using GsvClient;
using GsvClient.Models;

namespace WacomInkVerificationSample
{
    /// <summary>
    /// The main form for the application
    /// </summary>
    public partial class Main : Form
    {
        #region Fields

        private string mSignature = null;
        private readonly string _mLicense;
        private readonly IGsvClient _gsvClient;

        #endregion

        async Task MainCommonAsync()
        {
            try
            {                
                var request = new CreateClientRequest()
                {
                    NewClientName = "WacomInkVerificationSample"
                };                
                await _gsvClient.CreateClientAsync(request); // note will normally error as the client already exists.
            }
            catch (Exception e)
            {
                if (e?.InnerException?.InnerException is System.Net.Sockets.SocketException se && se.ErrorCode >= 10060 && se.ErrorCode <= 10065)
                {
                    MessageBox.Show(se.Message);
                    throw;
                }
            }

            try
            {
                var request = new GetTemplateListRequest();
                var list = await _gsvClient.GetTemplateListAsync(request);
                if (list?.TemplateNames != null)
                {
                    foreach (var name in list.TemplateNames)
                    {
                        lstTemplates.Items.Add(name);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Get template list failed: {e}");
            }
        }

        void MainCommon()
        {
            var task = Task.Run(async () => await MainCommonAsync());
            task.Wait();            
        }

        #region Constructor
        
        public Main(IGsvClient gsvClient)
        {
            InitializeComponent();

            _gsvClient = gsvClient;

            // Load license string
            // Note: The license supplied with this sample is for evaluation purposes and will expire 6 months 
            //       after the installation of the SDK components. 
            //       Please contact Wacom to obtain a full production license for use in your own code.
            try
            {
                _mLicense = File.ReadAllText("SampleLicense.txt").Trim();
            }
            catch { }

            MainCommon();
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Display About dialog
        /// </summary>
        private void OnAboutClick(object sender, EventArgs e)
        {            
            AboutDlg aboutBox = new AboutDlg(_gsvClient);
            aboutBox.ShowDialog(this);
        }

        /// <summary>
        /// Close the form and terminate the application
        /// </summary>
        private void OnExitClick(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();            
        }

        /// <summary>
        /// Add a new template
        /// </summary>
        private void OnAddClick(object sender, EventArgs e)
        {
            NewTemplateDlg newTemplateDlg = new NewTemplateDlg(_gsvClient);
            if (newTemplateDlg.ShowDialog(this) == DialogResult.OK)
            {
                int idx = lstTemplates.Items.Add(newTemplateDlg.TemplateName);
                lstTemplates.SelectedIndex = idx;
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
                var templateName = (string)lstTemplates.SelectedItem;
                if (MessageBox.Show($"Reset template '{templateName}'", "Reset Template",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    var request = new ResetTemplateRequest()
                    {
                        TemplateName = templateName
                    };

                    try
                    {
                        Task.Run(async () => await _gsvClient.ResetTemplateAsync(request)).Wait();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Reset Template failed {ex}");
                    }
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
                var templateName = (string)lstTemplates.SelectedItem;
                if (MessageBox.Show($"Delete template '{templateName}'", "Delete Template",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    DeleteTemplateRequest request = new DeleteTemplateRequest()
                    {
                        TemplateName = templateName
                    };

                    try 
                    {
                        Task.Run(async () => await _gsvClient.DeleteTemplateAsync(request)).Wait();
                        lstTemplates.Items.RemoveAt(lstTemplates.SelectedIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Delete failed: {ex}");
                    }                   
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
            OptionsDlg optionsDlg = new OptionsDlg(_gsvClient);
            optionsDlg.ShowDialog(this);
        }

        /// <summary>
        /// Displays the Status dialog showing details of the currently selected template
        /// </summary>
        private void OnStatusClick(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedIndex >= 0)
            {
                var templateName = (string)lstTemplates.SelectedItem;
                GetTemplateStatusRequest request = new GetTemplateStatusRequest()
                {
                    TemplateName = templateName
                };

                
                try
                {
                    var task = Task<GetTemplateStatusResponse>.Run(async () => await _gsvClient.GetTemplateStatusAsync(request));
                    task.Wait();
                    var status = task.Result;
                    var statusDlg = new TemplateStatusDlg(status.TemplateStatus);
                    statusDlg.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"GetTemplateStatus failed {ex}");
                }
                
            }
        }

        /// <summary>
        /// Captures a dynamic signature for subsequent enrollment or verification
        /// </summary>
        /// <remarks>Uses components from the Wacom Signaure SDK</remarks>
        private void OnCaptureClick(object sender, EventArgs e)
        {
            ClearSignature();

            var capture = new DynamicCapture();
            var sigCtl = new SigCtl();

            capture.Licence = _mLicense;
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
            mSignature = sigObj.SigText;
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
                    var data = File.ReadAllBytes(fileName);
                    ISigObj4 sigObj = new SigObj();
                    var r = sigObj.ReadEncodedBitmap(data);
                    if (r == ReadEncodedBitmapResult.ReadEncodedBitmapOK)
                    {
                        SetSignature(sigObj);
                    }
                    else
                    {
                        var stream = new MemoryStream(data);
                        try
                        {
                            var img = Image.FromStream(stream);
                            if (img.Width > imgSignature.Width || img.Height > imgSignature.Height)
                            {
                                float scale = Math.Min((float)imgSignature.Width / img.Width, (float)imgSignature.Height / img.Height);
                                img = new Bitmap(img, (int)(scale * img.Width), (int)(scale * img.Height));
                            }
                            imgSignature.Image = img;
                            mSignature = Convert.ToBase64String(data);
                        }
                        finally
                        {
                            if (stream != null)
                                stream.Dispose();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Verifies the current signature using the currently selected template
        /// </summary>
        private void VerifySignature()
        {
            if (mSignature != null && lstTemplates.SelectedIndex >= 0)
            {                
                var templateName = (string)lstTemplates.SelectedItem;

                var request = new VerifySignatureRequest()
                {
                    TemplateName = templateName,
                    SignatureData = mSignature
                };

                try
                {
                    var task = Task<VerifySignatureResponse>.Run(async () => await _gsvClient.VerifySignatureAsync(request));
                    task.Wait();
                    var verifySignatureResponse = task.Result;

                    var dlg = new ResultsDlg(verifySignatureResponse.VerificationResult);
                    dlg.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"VerifySignature failed: {ex}");
                }
            }
        }

        /// <summary>
        /// Loads dynamic signature data from a text file
        /// </summary>
        /// <param name="filename">Name of file containing base-64 encoded FSS data</param>
        private void LoadSigText(string filename)
        {
            ISigObj4 sigObj = new SigObj
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
            ISigObj4 sigObj = new SigObj
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

        #endregion
    }
}
