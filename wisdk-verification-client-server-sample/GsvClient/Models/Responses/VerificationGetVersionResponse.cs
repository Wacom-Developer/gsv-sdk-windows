namespace GsvClient.Models
{
    public class VerificationGetVersionResponse
    {
        /// <summary>
        /// The template name.
        /// </summary>
        public string Component_FileVersion { get; set; }

        /// <summary>
        /// Returns if the server is correctly licensed for operation
        /// </summary>
        public bool IsLicensed { get; set; }
    }
}


