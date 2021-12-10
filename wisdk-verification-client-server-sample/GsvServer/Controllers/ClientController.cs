using System.Threading.Tasks;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using GsvClient.Models;

using GsvPersistence.Interfaces;
using GsvPersistence.DataModels;


namespace GsvService.Controllers
{
    /// <summary>
    /// The GSV Authentication/Authorization Controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        private readonly ILogger<ClientController> _logger;
        private readonly IGsvRepository _dataRepository;
        private readonly IConfiguration _configuration;        
        
        private TemplateOptions _defaultTemplateOptions;

        /// <summary>
        ///  Lazy retrieves the default template options from the app configuration.
        /// </summary>
        private TemplateOptions DefaultTemplateOptions        
        {
            get
            {
                if (_defaultTemplateOptions == null)
                {
                    var options = new TemplateOptions();
                    var defaultConfigurationOptions = _configuration.GetSection("ConfigurationOptions");
                    options.ConfigurationOptions.TemplateSize = defaultConfigurationOptions.GetValue<ushort>("TemplateSize", 6);
                    options.ConfigurationOptions.EnrollmentScore = defaultConfigurationOptions.GetValue<float>("EnrollmentScore", 0);
                    options.ConfigurationOptions.UpdateInterval = defaultConfigurationOptions.GetValue<ushort>("UpdateInterval", 30);
                    options.ConfigurationOptions.SignatureStyle = defaultConfigurationOptions.GetValue<SignatureStyle>("UpdateInterval", SignatureStyle.AutoDetect);
                    options.ConfigurationOptions.IgnoreDateTime = defaultConfigurationOptions.GetValue<bool>("IgnoreDateTime", false);
                    options.ConfigurationOptions.ForceEnrollment = defaultConfigurationOptions.GetValue<bool>("ForceEnrollment", false);

                    var defaultImageOptions = _configuration.GetSection("ImageOptions");
                    options.ImageOptions.RemoveSpeckle = defaultImageOptions.GetValue<bool>("RemoveSpeckle", false);
                    options.ImageOptions.RemoveFold = defaultImageOptions.GetValue<bool>("RemoveFold", false);
                    options.ImageOptions.RemoveBox = defaultImageOptions.GetValue<bool>("RemoveBox", false);
                    options.ImageOptions.RemoveSigningLine = defaultImageOptions.GetValue<bool>("RemoveSigningLine", false);
                    options.ImageOptions.MinSigningLineLength = defaultImageOptions.GetValue<float>("MinSigningLineLength", 0);
                    options.ImageOptions.MaxSigningLineThickness = defaultImageOptions.GetValue<float>("MaxSigningLineThickness", 0);
                    options.ImageOptions.AdjustContrast = defaultImageOptions.GetValue<bool>("AdjustContrast", false);
                    options.ImageOptions.Contrast = defaultImageOptions.GetValue<short>("Contrast", 0);
                    options.ImageOptions.SetImageResolution = defaultImageOptions.GetValue<bool>("SetImageResolution", false);
                    options.ImageOptions.ImageResolution = defaultImageOptions.GetValue<ushort>("ImageResolution", 96);

                    _defaultTemplateOptions = options;
                }
                return _defaultTemplateOptions;
            }
        }

        /// <summary>
        /// TemplatesController constructor
        /// </summary>
        /// <param name="logger">The controller logger.</param>
        /// <param name="dataRepository">The data repository.</param>
        /// <param name="configuration">Provides defaults to initialize template options with.</param>
        public ClientController(ILogger<ClientController> logger, IGsvRepository dataRepository, IConfiguration configuration)
        {
            _logger = logger;
            _dataRepository = dataRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a new client with the given id
        /// </summary>
        /// <param name="request">provides the id to create</param>
        /// <returns></returns>
        [HttpPost("CreateClient")]
        public async Task<ActionResult> CreateClient([FromBody] CreateClientRequest request)
        {
            _logger?.LogInformation(@$"Received {nameof(CreateClientRequest)}", request);

            // For a real implementation, ensure that the incoming request is authorised to create a client!
            //
            //var clientDataModel = await _dataRepository.GetByNameAsync<ClientDataModel>(request.ClientName);
            //if (clientDataModel == null)
            //{
            //    return Unauthorized();
            //}
            

            var client = await _dataRepository.GetByNameAsync<ClientDataModel>(request.ClientName);
            if (client != null)
            {
                // Client already exists!
                return Conflict();
            }

            TemplateOptions options = new TemplateOptions();

            if (request.TemplateOptions != null)
            {
                options = request.TemplateOptions;
            }
            else
            {
                options = DefaultTemplateOptions;
            }

            string json = JsonSerializer.Serialize<TemplateOptions>(options, _jsonSerializerOptions);

            var clientDataModel = await _dataRepository.AddAsync<ClientDataModel>(new ClientDataModel(request.NewClientName, json));

            return Ok();
        }

        /// <summary>
        /// Retrieves the default template options of the client
        /// </summary>
        /// <param name="request">Provides the client credentials</param>
        /// <returns>The template options</returns>
        [HttpPost("GetTemplateOptions")]
        public async Task<ActionResult<GetClientTemplateOptionsResponse>> GetTemplateOptions(GetClientTemplateOptionsRequest request)
        {
            var clientDataModel = await _dataRepository.GetByNameAsync<ClientDataModel>(request.ClientName);
            if (clientDataModel == null)
            {
                return Unauthorized();
            }

            TemplateOptions options = System.Text.Json.JsonSerializer.Deserialize<TemplateOptions>(clientDataModel.TemplateOptions, _jsonSerializerOptions);

            var response = new GetClientTemplateOptionsResponse()
            {
                Options = options
            };

            return new OkObjectResult(response);
        }



        /// <summary>
        /// Updates the client's template options.
        /// </summary>
        /// <remarks>Note this does not change the options for existing templates</remarks>
        /// <param name="request">The client and new options to use.</param>
        /// <returns>Ok on success</returns>
        [HttpPost("UpdateTemplateOptions")]
        public async Task<IActionResult> UpdateTemplateOptions(UpdateClientTemplateOptionsRequest request)
        {

            var clientDataModel = await _dataRepository.GetByNameAsync<ClientDataModel>(request.ClientName);
            if (clientDataModel == null)
            {
                return Unauthorized();
            }

            clientDataModel.UpdateTemplateOptionsJson(JsonSerializer.Serialize<TemplateOptions>(request.Options, _jsonSerializerOptions));
            
            await _dataRepository.UpdateAsync(clientDataModel);

            return Ok();
        }

    }
}
