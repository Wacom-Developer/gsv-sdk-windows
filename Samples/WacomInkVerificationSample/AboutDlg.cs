/*  AboutDlg.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */

using System.Reflection;
using System.Windows.Forms;

using WacomVerification;

namespace WacomVerificationSample
{
    /// <summary>
    /// Application About box
    /// Displays app and component version numbers
    /// </summary>
    public partial class AboutDlg : Form
    {
        public AboutDlg()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            lblVersion.Text = assembly.GetName().Version.ToString();

            SignatureEngine sigEngine = new SignatureEngine();

            lblComponentVer.Text = sigEngine.GetProperty("Component_FileVersion").ToString();
        }
    }
}
