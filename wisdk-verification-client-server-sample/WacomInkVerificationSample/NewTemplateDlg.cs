/*  NewTemplateDlg.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using GsvClient;
using GsvClient.Models;

namespace WacomInkVerificationSample
{
    /// <summary>
    /// Dialog box for the input of a template name
    /// </summary>
    public partial class NewTemplateDlg : Form
    {
        IGsvClient _gsvClient;

        public NewTemplateDlg(IGsvClient gsvClient)
        {
            _gsvClient = gsvClient;
            InitializeComponent();
        }

        public string TemplateName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CreateTemplateRequest request = new CreateTemplateRequest()
            {
                TemplateName = TemplateName
            };

            Task.Run(async () => await _gsvClient.CreateTemplateAsync(request)).Wait();
        }
    }
}
