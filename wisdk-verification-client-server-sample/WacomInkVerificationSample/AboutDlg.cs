using System;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

using GsvClient;

namespace WacomInkVerificationSample
{
    /// <summary>
    /// Application About box
    /// Displays app and component version numbers
    /// </summary>
    public partial class AboutDlg : Form
    {        
        private readonly SynchronizationContext _synchronizationContext;
        private Task _task;

        private async Task GetVersionAsync(IGsvClient gsvClient)
        {
            string server_Component_FileVersion;
            string server_IsLicensed;

            try
            {                
                server_Component_FileVersion = (await gsvClient.VerificationGetVersionAsync()).Component_FileVersion;
            }
            catch (Exception e)
            {
                server_Component_FileVersion = $"error: {e}";
            }

            try
            {
                server_IsLicensed = (await gsvClient.VerificationGetVersionAsync()).IsLicensed ? "Yes" : "No";
            }
            catch (Exception e)
            {
                server_IsLicensed = $"error: {e}";
            }

            _synchronizationContext.Post((o) => { 
                lblServerComponentVersion.Text = server_Component_FileVersion;
                lblServerIsLicensed.Text = server_IsLicensed;
            }, null);            
        }

        public AboutDlg(IGsvClient gsvClient)
        {
            InitializeComponent();
            _synchronizationContext = SynchronizationContext.Current;

            Assembly assembly = Assembly.GetExecutingAssembly();
            lblVersion.Text = assembly.GetName().Version.ToString();

            textBoxServerURL.Text = gsvClient.GetServerAddress();
            textBoxServerURL.SelectionStart = 0;
            textBoxServerURL.SelectionLength = 0;


            _task = Task.Run(async () => await GetVersionAsync(gsvClient));
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {                
                _task.Dispose();

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
