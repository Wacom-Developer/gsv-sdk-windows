/*  NewTemplateDlg.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System;
using System.Windows.Forms;

namespace WacomVerificationSample
{
    /// <summary>
    /// Dialog box for the input of a template name
    /// </summary>
    public partial class NewTemplateDlg : Form
    {
        public NewTemplateDlg()
        {
            InitializeComponent();
        }

        public string TemplateName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

    }
}
