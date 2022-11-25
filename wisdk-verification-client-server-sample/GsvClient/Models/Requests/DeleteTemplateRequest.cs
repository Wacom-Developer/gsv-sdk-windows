namespace GsvClient.Models
{
    /// <summary>
    /// The request model for deleting a template.
    /// </summary>
    public class DeleteTemplateRequest
    {
        public string ClientName { get; set; }

        /// <summary>
        /// The template name.
        /// </summary>
        public string TemplateName { get; set; }
    }
}
