using System;
using System.Security.Cryptography;
using System.Text;

namespace GsvPersistence.DataModels
{
    /// <summary>
    /// The Client Data Model
    /// </summary>
    public class ClientDataModel : DataModelBase
    {
        protected string _templateOptions;

        public string TemplateOptions { get => _templateOptions; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientDataModel()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The client's name.</param>
        public ClientDataModel(string name)
            : base()
        {
            _name = name;           
        }

        public ClientDataModel(string name, string templateOptions)
            : base()
        {
            _name = name;
            _templateOptions = templateOptions;
        }

        public void UpdateTemplateOptionsJson(string templateOptions)
        {
            _templateOptions = templateOptions;
        }

    }
}
