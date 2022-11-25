namespace GsvClient.Models
{
    /// <summary>
    /// The request model for creating a new template.
    /// </summary>
    public class CreateClientRequest
    {
        public string ClientName { get; set; }

        /// <summary>
        /// The client to create.
        /// </summary>
        public string NewClientName { get; set; }

        /// <summary>
        /// The default template options to assign to the new client name. Optional.
        /// </summary>
        public TemplateOptions TemplateOptions { get; set; }
    }
}
