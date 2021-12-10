namespace GsvClient.Models
{
    public class VerifySignatureResponse
    {
        /// <summary>
        /// The template name.
        /// </summary>
        public string TemplateName { get; set; }

        public VerificationResult VerificationResult { get; set; }
    }
}
