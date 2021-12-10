using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using GsvClient;
using GsvClient.Models;

namespace WacomInkVerificationSample
{
    /// <summary>
    /// Example client that illustrates how you could override the base in order to log an error condition.
    /// </summary>
    public class GsvClient : GsvClientBase
    {
        private ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">provides server information</param>
        /// <param name="logger">optional logger</param>
        public GsvClient(GsvClientConfiguration configuration, ILogger logger = null)
            : base(configuration)
        {
            _logger = logger;
        }

        public override async Task<VerifySignatureResponse> VerifySignatureAsync(VerifySignatureRequest request)
        {
            try
            {
                var result = await base.VerifySignatureAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
