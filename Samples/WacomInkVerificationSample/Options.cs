/*  Options.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System;
using System.IO;

using WacomVerification;

namespace WacomVerificationSample
{
    /// <summary>
    /// Singleton object for template and application options and settings
    /// </summary>
    class Options
    {
        #region Properties

        /// <summary>
        /// Name of folder in which templates are to be stored
        /// </summary>
        public string TemplateFolder
        {
            get { return Properties.Settings.Default.TemplateFolder; }
            set { Properties.Settings.Default.TemplateFolder = value; }
        }

        /// <summary>
        /// Template configuration options 
        /// </summary>
        /// <remarks>Options which apply to all signature types</remarks>
        public ConfigurationOptions ConfigurationOptions { get; private set; } = new ConfigurationOptions();

        /// <summary>
        /// Image processing options
        /// </summary>
        /// <remarks>Options which are used to control the way in which static images are processed</remarks>
        public ImageOptions ImageOptions { get; private set; } = new ImageOptions();

        #endregion

        #region Public interface

        /// <summary>
        /// Returns the singleton Options object
        /// </summary>
        public static Options Instance { get; private set; } = new Options();

        /// <summary>
        /// Saves current options to application settings
        /// </summary>
        public void Save()
        {
            UpdateSettings();
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Private constructor. Initializes values from application user settings
        /// </summary>
        private Options()
        {
            var settings = Properties.Settings.Default;

            // The first time the app is run, TemplateFolder will be blank
            if (string.IsNullOrEmpty(settings.TemplateFolder))
            {
                // Set default template folder
                settings.TemplateFolder = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WacomVerification");

                UpdateSettings();
            }
            else
            {
                // Restore values from settings
                ConfigurationOptions.EnrollmentScore = settings.EnrollmentScore;
                ConfigurationOptions.ForceEnrollment = settings.ForceEnrollment;
                ConfigurationOptions.IgnoreDateTime = settings.IgnoreDateTime;
                ConfigurationOptions.TemplateSize = settings.TemplateSize;
                ConfigurationOptions.UpdateInterval = settings.UpdateInterval;

                ImageOptions.AdjustContrast = settings.AdjustContrast;
                ImageOptions.Contrast = settings.Contrast;
                ImageOptions.ImageResolution = settings.ImageResolution;
                ImageOptions.MaxSigningLineThickness = settings.MaxSigningLineThickness;
                ImageOptions.MinSigningLineLength = settings.MinSigningLineLength;
                ImageOptions.RemoveBox = settings.RemoveBox;
                ImageOptions.RemoveFold = settings.RemoveFold;
                ImageOptions.RemoveSigningLine = settings.RemoveSigningLine;
                ImageOptions.RemoveSpeckle = settings.RemoveSpeckle;
                ImageOptions.SetImageResolution = settings.SetImageResolution;
            }
        }

        /// <summary>
        /// Copy current values to application settings
        /// </summary>
        private void UpdateSettings()
        {
            var settings = Properties.Settings.Default;

            settings.EnrollmentScore = ConfigurationOptions.EnrollmentScore;
            settings.ForceEnrollment = ConfigurationOptions.ForceEnrollment;
            settings.IgnoreDateTime = ConfigurationOptions.IgnoreDateTime;
            settings.TemplateSize = ConfigurationOptions.TemplateSize;
            settings.UpdateInterval = ConfigurationOptions.UpdateInterval;

            settings.AdjustContrast = ImageOptions.AdjustContrast;
            settings.Contrast = ImageOptions.Contrast;
            settings.ImageResolution = ImageOptions.ImageResolution;
            settings.MaxSigningLineThickness = ImageOptions.MaxSigningLineThickness;
            settings.MinSigningLineLength = ImageOptions.MinSigningLineLength;
            settings.RemoveBox = ImageOptions.RemoveBox;
            settings.RemoveFold = ImageOptions.RemoveFold;
            settings.RemoveSigningLine = ImageOptions.RemoveSigningLine;
            settings.RemoveSpeckle = ImageOptions.RemoveSpeckle;
            settings.SetImageResolution = ImageOptions.SetImageResolution;
        }

        #endregion
    }
}
