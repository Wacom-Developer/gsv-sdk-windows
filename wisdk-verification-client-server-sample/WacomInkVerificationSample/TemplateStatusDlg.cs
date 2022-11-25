/*  TemplateStatusDlg.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System.Windows.Forms;
using GsvClient.Models;


namespace WacomInkVerificationSample
{
    /// <summary>
    /// Displays template status information
    /// </summary>
    public partial class TemplateStatusDlg : Form
    {
        public TemplateStatusDlg(TemplateStatus status)
        {
            InitializeComponent();

            txtEnrollSizeDyn.Text = status.DynamicStatus.EnrollmentSize.ToString();
            txtEnrollStateDyn.Text = status.DynamicStatus.EnrollmentState.ToString();
            txtNumSigsDyn.Text = status.DynamicStatus.NumSignatures.ToString();

            txtEnrollSizeStat.Text = status.StaticStatus.EnrollmentSize.ToString();
            txtEnrollStateStat.Text = status.StaticStatus.EnrollmentState.ToString();
            txtNumSigsStat.Text = status.StaticStatus.NumSignatures.ToString();
        }
    }
}
