namespace GsvPersistence.DataModels
{
    /// <summary>
    /// The signature template data model.
    /// </summary>
    public class TemplateDataModel : DataModelBase
    {
        private string _data;

        /// <summary>
        /// The id of the client that created this template.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The signature template data.
        /// </summary>
        public string Data => _data;

        /// <summary>
        /// Constructor
        /// </summary>
        protected TemplateDataModel()
            : base()
        {
        }

        /// <summary>
        /// Contrusctor
        /// </summary>
        /// <param name="name">The signature template name.</param>
        /// <param name="data">The initial signature template data.</param>
        public TemplateDataModel(string name, string data)
            : base()
        {
            _name = name;
            _data = data;
        }

        /// <summary>
        /// Updates the signature template data
        /// </summary>
        /// <param name="signatureData">The most recent signature template data.</param>
        public void UpdateData(string signatureData)
        {
            _data = signatureData;
        }

        /// <summary>
        /// Resets the signature template data.
        /// </summary>
        public void ResetData()
        {
            _data = string.Empty;
        }
    }
}
