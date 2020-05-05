/*  Template.cs
 *   
 *  Copyright © 2020 Wacom Co.,Ltd.
 */
using System.IO;

using WacomVerification;

namespace WacomVerificationSample
{
    /// <summary>
    /// Handles the storage of template data
    /// </summary>
    class Template
    {
        #region Fields
        private string mData;   // Base-64 encoded template data
        private const string mDeletedMsg = "Template has been deleted";
        #endregion

        #region Properties

        /// <summary>
        /// Base-64 encoded template data
        /// </summary>
        public string Data
        {
            get 
            {
                if (mData == null)
                    throw new System.InvalidOperationException(mDeletedMsg);
                return mData; 
            }
            set
            {
                mData = value;
                SaveToFile();
            }
        }

        /// <summary>
        /// The name of the template
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Default file extension for template files
        /// </summary>
        public const string Extn = ".tpl";

        #endregion

        #region Public interface

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of template</param>
        public Template(string name)
        {
            Name = name;
            Reset();
        }

        /// <summary>
        /// Loads a template from a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Template object</returns>
        /// <remarks>the name of template is the name of the file</remarks>
        public static Template LoadFromFile(string filename)
        {
            Template template = new Template
            {
                Name = Path.GetFileNameWithoutExtension(filename),
                Data = File.ReadAllText(filename)
            };
            return template;
        }

        /// <summary>
        /// Clears the template and reinitilaizes using the current template options
        /// </summary>
        public void Reset()
        {
            SignatureEngine sigEngine = new SignatureEngine();

            Data = sigEngine.CreateTemplate(Options.Instance.ConfigurationOptions, Options.Instance.ImageOptions);
            SaveToFile();
        }

        /// <summary>
        /// Deletes the template file and makes this object 'invalid'
        /// </summary>
        /// <remarks></remarks>
        public void Delete()
        {
            File.Delete(FileName());
            // Invalidate object
            Name = null;
            mData = null;
        }

        #endregion

        #region ListBox support

        /// <summary>
        /// Returns the name of the template
        /// </summary>
        public override string ToString() { return Name; }

        /// <summary>
        /// Override to support check for duplicate names in template list
        /// </summary>
        /// <param name="obj">Template or template name to compare</param>
        /// <returns>true if names match</returns>
        public override bool Equals(object obj)
        {
            if ((obj is string name))
                return name == Name;
            else if (!(obj is Template other))
                return false;
            else
                return other.Name == Name;
        }

        /// <summary>
        /// Required due to override of Equals method
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Private constructor for use in LoadFromFile
        /// </summary>
        private Template()
        {
        }

        /// <summary>
        /// Saves template data to file
        /// </summary>
        private void SaveToFile()
        {
            if (Name == null)
                throw new System.InvalidOperationException(mDeletedMsg);

            // Ensure folder exists
            Directory.CreateDirectory(Options.Instance.TemplateFolder);
            // Create file
            File.WriteAllText(FileName(), Data);
        }

        /// <summary>
        /// Formats file name
        /// </summary>
        /// <returns></returns>
        private string FileName()
        {
            return Path.ChangeExtension(Path.Combine(Options.Instance.TemplateFolder, Name), Extn);
        }

        #endregion
    }
}
