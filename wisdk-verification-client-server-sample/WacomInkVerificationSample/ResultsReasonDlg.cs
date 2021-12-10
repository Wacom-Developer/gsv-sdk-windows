using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WacomInkVerificationSample
{
    public partial class ResultsReasonDlg : Form
    {
        public ResultsReasonDlg(string reason)
        {
            InitializeComponent();
            textBoxReason.Text = reason;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // https://stackoverflow.com/questions/3421453/why-is-text-in-textbox-highlighted-selected-when-form-is-displayed
            //
            this.textBoxReason.SelectionStart = 0;
            this.textBoxReason.SelectionLength = 0;            
        }
    }
}
