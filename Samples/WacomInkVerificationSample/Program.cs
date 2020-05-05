/*  Program.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System;
using System.Windows.Forms;

namespace WacomVerificationSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
