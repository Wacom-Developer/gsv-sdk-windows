
namespace GsvClient.Models
{
    /// <summary>
    /// The GSV Client Configuration.
    /// </summary>
    public class GsvClientConfiguration
    {
        /// <summary>
        /// The GSV Server Ednpoint.
        /// </summary>
        public string ServerAddress { get; set; }

        /// <summary>
        /// The name of the client that is connecting to the server.
        /// </summary>
        public string ClientName { get; set; }

    }
}
