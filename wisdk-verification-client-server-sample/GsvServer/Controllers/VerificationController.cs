using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using GsvClient.Models;

namespace GsvService.Controllers
{
    /// <summary>
    /// The GSV Singature Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class VerificationController : ControllerBase
    {
        private readonly ILogger<VerificationController> _logger;
        private readonly GsvSignatureEngineService _gsvSignatureEngineService;

        /// <summary>
        /// VerificationController constructor
        /// </summary>
        /// <param name="logger">The controller logger.</param>
        /// <param name="gsvSignatureEngineService">The signature engine worker.</param>
        public VerificationController(ILogger<VerificationController> logger, GsvSignatureEngineService gsvSignatureEngineService)
        {
            _logger = logger;
            _gsvSignatureEngineService = gsvSignatureEngineService;
        }

        /// <summary>
        /// Returns the version of the underlying WacomVerification component
        /// </summary>
        /// <returns>The component file version string</returns>
        [HttpPost("GetVersion")]
        public Task<ActionResult<VerificationGetVersionResponse>> GetVersion()
        {
            _logger?.LogInformation($"Received {nameof(GetVersion)}");

            var result = _gsvSignatureEngineService.Component_FileVersion;

            var response = new VerificationGetVersionResponse()
            {
                Component_FileVersion = result,
                IsLicensed = _gsvSignatureEngineService.IsLicensed
            };

            return Task.FromResult((ActionResult<VerificationGetVersionResponse>)new OkObjectResult(response));
        }


        /// <summary>
        /// Verifies a signature with a given template.
        /// </summary>
        /// <param name="request">The signature verification request.</param>
        /// <returns>SignatureVerificationResponse</returns>
        [HttpPost("VerifySignature")]
        public async Task<ActionResult<VerifySignatureResponse>> VerifySignature([FromBody] VerifySignatureRequest request)
        {
            _logger?.LogInformation($"Received {nameof(VerifySignatureRequest)}", request);
            
            var result = await _gsvSignatureEngineService.VerifySignatureAsync(request.TemplateName, request.SignatureData);

            var response = new VerifySignatureResponse()
            {
                TemplateName = request.TemplateName,
                VerificationResult = result                
            };

            return new OkObjectResult(response);
        }
    }
}
