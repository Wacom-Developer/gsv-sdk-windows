namespace GsvClient.Models
{
    /// <summary>
    /// The request model for creating a new template.
    /// </summary>
    public class UpdateClientTemplateOptionsRequest
    {
        public string ClientName { get; set; }

        /// <summary>
        /// Options
        /// </summary>
        public TemplateOptions Options { get; set; }
    }
}
