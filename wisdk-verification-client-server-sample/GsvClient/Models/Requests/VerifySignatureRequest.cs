namespace GsvClient.Models
{
    /// <summary>
    /// The request model for verifying signatures with a specified template.
    /// </summary>
    public class VerifySignatureRequest
    {
        public string ClientName { get; set; }

        /// <summary>
        /// The template name.
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// The signature data represented in Base64 string.
        /// </summary>
        public string SignatureData { get; set; }
    }
}
