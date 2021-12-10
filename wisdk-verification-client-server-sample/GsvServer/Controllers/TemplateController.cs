using System.Threading.Tasks;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using GsvClient.Models;

using GsvPersistence.Interfaces;
using GsvPersistence.DataModels;


namespace GsvService.Controllers
{
    /// <summary>
    /// The GSV Temmplates Controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : ControllerBase
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        private readonly ILogger<TemplateController> _logger;
        private readonly IGsvRepository _dataRepository;        
        private readonly GsvSignatureEngineService _signatureVerificationWorker;        
                
        /// <summary>
        /// TemplatesController constructor
        /// </summary>
        /// <param name="logger">The controller logger.</param>
        /// <param name="dataRepository">The data repository.</param>
        /// <param name="signatureVerificationWorker">The signature worker.</param>        
        public TemplateController(ILogger<TemplateController> logger,
            IGsvRepository dataRepository,
            GsvSignatureEngineService signatureVerificationWorker)
        {
            _logger = logger;
            _dataRepository = dataRepository;            
            _signatureVerificationWorker = signatureVerificationWorker;            
        }

        /// <summary>
        /// Creates a template with a given name.
        /// </summary>
        /// <param name="request">The template creation request.</param>
        /// <returns>The added entity.</returns>
        [HttpPost("GetTemplateList")]
        public async Task<ActionResult<GetTemplateListResponse>> GetTemplateList(GetTemplateListRequest request)
        {
            _logger?.LogInformation(@$"Received {nameof(GetTemplateList)}");

            var clientDataModel = await _dataRepository.GetByNameAsync<ClientDataModel>(request.ClientName);
            if (clientDataModel == null)
            {
                return Unauthorized();
            }

            var list = await _dataRepository.GetAllAsync<TemplateDataModel>();
            var names = new System.Collections.Generic.List<string>();
            foreach (var x in list)
            {
                names.Add(x.Name);
            }

            var response = new GetTemplateListResponse()
            {
                TemplateNames = names
            };

            return new OkObjectResult(response);
        }


        /// <summary>
        /// Creates a template with a given name.
        /// </summary>
        /// <param name="request">The template creation request.</param>
        /// <returns>The added entity.</returns>
        [HttpPost("CreateTemplate")]
        public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateRequest request)
        {
            _logger?.LogInformation(@$"Received {nameof(CreateTemplateRequest)}", request);

            var clientDataModel = await _dataRepository.GetByNameAsync<ClientDataModel>(request.ClientName);
            if (clientDataModel == null)
            {
                return Unauthorized();
            }

            TemplateOptions templateOptions = JsonSerializer.Deserialize<TemplateOptions>(clientDataModel.TemplateOptions, _jsonSerializerOptions);
            
            string templateData = _signatureVerificationWorker.CreateTemplate(request.TemplateName, templateOptions);
            var newTemplate = new TemplateDataModel(request.TemplateName, templateData)
            {
                ClientId = clientDataModel.Id
            };
            var addedEntity = await _dataRepository.AddAsync(newTemplate);
            return new OkObjectResult(addedEntity);
        }

        /// <summary>
        /// Requests deletion of all template data for a specified tempate name.
        /// </summary>
        /// <param name="request">The template deletion request.</param>
        /// <returns>ActionResult</returns>
        [HttpPost("DeleteTemplate")]
        public async Task<IActionResult> DeleteTemplate([FromBody] DeleteTemplateRequest request)
        {
            _logger?.LogInformation(@$"Received {nameof(DeleteTemplateRequest)}", request);

            var clientDataModel = await _dataRepository.GetByNameAsync<ClientDataModel>(request.ClientName);
            if (clientDataModel == null)
            {
                return Unauthorized();
            }

            var existingTemplate = await _dataRepository.GetByNameAsync<TemplateDataModel>(request.TemplateName);

            if (existingTemplate.ClientId.Equals(clientDataModel.Id))
            {
                await _dataRepository.RemoveAsync<TemplateDataModel>(existingTemplate.Id);
            }
            else
            {
                return Unauthorized();
            }

            return Ok();
        }

        /// <summary>
        /// Requests data deletion for a specified template by name.
        /// </summary>        
        /// <returns>ActionResult</returns>
        [HttpPost("ResetTemplate")]
        public async Task<IActionResult> ResetTemplate([FromBody] ResetTemplateRequest request)
        {
            _logger?.LogInformation(@$"Received {nameof(ResetTemplateRequest)}", request);

            var clientDataModel = await _dataRepository.GetByNameAsync<ClientDataModel>(request.ClientName);
            if (clientDataModel == null)
            {
                return Unauthorized();
            }

            var template = await _dataRepository.GetByNameAsync<TemplateDataModel>(request.TemplateName);

            if (!template.ClientId.Equals(clientDataModel.Id))
            {
                return Unauthorized();
            }
                       

            TemplateOptions options = JsonSerializer.Deserialize<TemplateOptions>(clientDataModel.TemplateOptions, _jsonSerializerOptions);
            string templateData = _signatureVerificationWorker.CreateTemplate(request.TemplateName, options);
            template.UpdateData(templateData);
            await _dataRepository.UpdateAsync(template);

            return Ok();
        }


        /// <summary>
        /// Retrieves the current state of the named template.
        /// </summary>
        /// <param name="request">Provides the name of the template to retrieve data for.</param>
        /// <returns></returns>
        [HttpPost("GetTemplateStatus")]
        public async Task<ActionResult<GetTemplateStatusResponse>> GetTemplateStatus(GetTemplateStatusRequest request)
        {
            _logger?.LogInformation(@$"Received {nameof(GetTemplateStatus)}");

            var clientDataModel = await _dataRepository.GetByNameAsync<ClientDataModel>(request.ClientName);
            if (clientDataModel == null)
            {
                return Unauthorized();
            }

            var templateStatus = await _signatureVerificationWorker.GetTemplateStatusAsync(request.TemplateName);
            
            var response = new GetTemplateStatusResponse()
            {
                TemplateStatus = templateStatus
            };

            return new OkObjectResult(response);
        }
    }
}
