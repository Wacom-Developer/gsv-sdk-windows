namespace GsvClient.Models
{
    /// <summary>
    /// The request model for creating a new template.
    /// </summary>
    public class GetTemplateStatusRequest
    {
        public string ClientName { get; set; }

        /// <summary>
        /// The template name.
        /// </summary>
        public string TemplateName { get; set; }
    }
}
