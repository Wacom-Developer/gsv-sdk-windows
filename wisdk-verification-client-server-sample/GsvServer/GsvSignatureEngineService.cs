using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using GsvClient.Models;

using GsvPersistence.Interfaces;
using GsvPersistence.DataModels;

namespace GsvService
{
    /// <summary>
    /// The GSV Signature Verification Worker, which handles signature verifications.
    /// </summary>
    public class GsvSignatureEngineService : IHostedService
    {
        private ILogger<GsvSignatureEngineService> _logger;
        private IGsvRepository _dataRepository;
        private IConfiguration _configuration;        
        private WacomVerification.SignatureEngine _signatureEngine;

        private WacomVerification.SignatureEngine SignatureEngine_NoLicenseCheck
        {
            get
            {
                if (_signatureEngine == null)
                {
                    _signatureEngine = new WacomVerification.SignatureEngine();
                }
                return _signatureEngine;
            }
        }

        private WacomVerification.SignatureEngine SignatureEngine
        {
            get
            {
                if (_signatureEngine == null)
                {
                    _signatureEngine = new WacomVerification.SignatureEngine();
                }

                var license = _signatureEngine.License;
                if (license == null || license.Length == 0)
                {
                    license = _configuration.GetValue<string>("License");
                    if (license == null || license.Length == 0)
                    {
                        _logger.LogError("The Wacom Signature Engine license is not in the configuration file");
                        throw new UnauthorizedAccessException("There is no license in the configuration file");
                    }

                    _signatureEngine.License = license;
                    
                    if (!_signatureEngine.IsLicensed)
                    {
                        _signatureEngine = null;
                        _logger.LogError("The Wacom Signature Engine does not accept the license provided in the configuration file");
                        throw new UnauthorizedAccessException("The Wacom Signature Engine is not licensed");
                    }
                }
                return _signatureEngine;
            }
        }


        /// <summary>
        /// Constructor for the micro service
        /// </summary>
        /// <param name="logger">logging engine</param>
        /// <param name="dataRepository">database for templates</param>
        /// <param name="configuration">for retrieving the license</param>
        public GsvSignatureEngineService(
            ILogger<GsvSignatureEngineService> logger,
            IGsvRepository dataRepository,
            IConfiguration configuration)
        {
            _logger = logger;
            _dataRepository = dataRepository;
            _configuration = configuration;  
            
            // lazy create the SignatureEngine in order to keep the constructor light and fast.
        }
        

        /// <summary>
        /// Retrieves the version of the underlying Signature Verification Engine component.
        /// </summary>
        public string Component_FileVersion
        {
            get
            {
                var signatureEngine = SignatureEngine_NoLicenseCheck;
                return signatureEngine.GetProperty("Component_FileVersion");
            }
        }

        /// <summary>
        /// Retrieves is the license string is valid
        /// </summary>
        public bool IsLicensed
        {
            get
            {
                var signatureEngine = SignatureEngine_NoLicenseCheck;
                return signatureEngine.IsLicensed;
            }
        }

        /// <summary>
        /// Retrieves information about the named template
        /// </summary>
        /// <param name="templateName">The name of the template</param>        
        /// <returns>The current state of the template.</returns>
        public async Task<GsvClient.Models.TemplateStatus> GetTemplateStatusAsync(string templateName)
        {
            var template = await _dataRepository.GetByNameAsync<TemplateDataModel>(templateName);

            var signatureEngine = SignatureEngine;
            
            var templateStatus = signatureEngine.GetTemplateStatus(template.Data);

            return templateStatus.ToGsvClientModel();
        }

        /// <summary>
        /// Retrieves a template from the repository and verifies it against the provided signature
        /// </summary>
        /// <param name="templateName">The name of the template to retrieve from the repository</param>
        /// <param name="signatureData">The signature in FSS string format</param>        
        /// <returns></returns>
        public async Task<GsvClient.Models.VerificationResult> VerifySignatureAsync(string templateName, string signatureData)
        {
            try
            {
                var template = await _dataRepository.GetByNameAsync<TemplateDataModel>(templateName);
                if (template == null)
                {
                    throw new ArgumentException($"{nameof(templateName)} '{templateName}' not found");
                }                    
            
                var signatureEngine = SignatureEngine;

                string templateData = template.Data;

                WacomVerification.VerificationResult result = null;                
                Exception e = null;
                try
                {
                    result = signatureEngine.VerifySignature(templateData, signatureData);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
                
                var reason = (string)signatureEngine.GetProperty("VerifySignature_Reason");

                if (result != null)
                {
                    // Update the template data model and write it back to the repository
                    template.UpdateData(result.UpdatedTemplate);
                    await _dataRepository.UpdateAsync(template);

                    return new GsvClient.Models.VerificationResult()
                    {
                        Score = result.Score,
                        Engine = (ComparisonType)result.Engine,
                        Reason = reason,
                        Inconsistency = (InconsistencyType)result.Inconsistency,
                        State = result.State.ToGsvClientModel(),
                        Complexity = result.Complexity,
                        //UpdatedTemplate = result.UpdatedTemplate, // no need to return the template itself
                        MixedScore = result.MixedScore
                    };
                }                
                else if (e == null)
                {
                    return new GsvClient.Models.VerificationResult()
                    {
                        Reason = reason
                    };
                }                

                throw e;                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                throw;
            }
        }

        
        /// <summary>
        /// Creates a template in the database.
        /// </summary>
        /// <param name="name">The signature template name.</param>
        /// <param name="options">Parameters to use</param>
        /// <returns>The created template data</returns>
        public string CreateTemplate(string name, TemplateOptions options)
        {
            WacomVerification.ConfigurationOptions configurationOptions = options.ConfigurationOptions.ToWacomVerification();
            WacomVerification.ImageOptions imageOptions = options.ImageOptions.ToWacomVerification();

            //_signatureEngine = new WacomVerification.SignatureEngine();
            var license = _configuration.GetValue<string>("License");

            WacomVerification.SignatureEngine signatureEngine = new WacomVerification.SignatureEngine();
            signatureEngine.License = license;

            var tpl = signatureEngine.CreateTemplate(configurationOptions, imageOptions);

            return tpl;
        }

        /// <summary>
        /// Starts the service, required by IHostedService
        /// </summary>
        /// <param name="cancellationToken">not used</param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;        
        }

        /// <summary>
        /// Stops the service, required by IHostedService
        /// </summary>
        /// <param name="cancellationToken">not used</param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
