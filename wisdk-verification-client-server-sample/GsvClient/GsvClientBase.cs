using GsvClient.Models;

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

using System.Threading.Tasks;

namespace GsvClient
{
    public class GsvClientBase : IGsvClient
    {
        private GsvClientConfiguration _gsvClientConfiguration;
        private bool _disposed = false;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        protected HttpClient _httpClient;
        
        public string GetServerAddress()
        {
            return _gsvClientConfiguration.ServerAddress;
        }

        /// <summary>
        /// The GSV Client constructor.
        /// </summary>
        /// <param name="configuration"><seealso cref="GsvClientConfiguration"/></param>
        public GsvClientBase(GsvClientConfiguration configuration)
        {
            _gsvClientConfiguration = configuration;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            CreateClient();
        }

        /// <summary>
        /// Creates a GSV Client by a given configuration.
        /// </summary>
        /// <param name="configuration"><seealso cref="GsvClientConfiguration"/></param>
        private void CreateClient(GsvClientConfiguration configuration = null)
        {
            if (configuration != null)
            {
                _gsvClientConfiguration = configuration;
            }

            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(_gsvClientConfiguration.ServerAddress);
            }
        }

        /// <summary>
        /// Creates a GSV signature template.
        /// </summary>
        /// <param name="request"><seealso cref="CreateTemplateRequest"/></param>
        /// <returns>The created signature template object.</returns>
        public virtual async Task CreateTemplateAsync(CreateTemplateRequest request)
        {
            if (request.ClientName == null) request.ClientName = _gsvClientConfiguration.ClientName;

            string requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);

            var httpResponse = await _httpClient.PostAsync("/Template/CreateTemplate", new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }
        }

        public virtual async Task CreateClientAsync(CreateClientRequest request)
        {
            if (request.ClientName == null) request.ClientName = _gsvClientConfiguration.ClientName;

            string requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);

            var httpResponse = await _httpClient.PostAsync("/Client/CreateClient", new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }
        }


        /// <summary>
        /// Resets the signature template data, erasing all current template data.
        /// </summary>
        /// <param name="request"><seealso cref="ResetTemplateRequest"/></param>
        /// <returns>The reset signature template object.</returns>
        public virtual async Task ResetTemplateAsync(ResetTemplateRequest request)
        {
            if (request.ClientName == null) request.ClientName = _gsvClientConfiguration.ClientName;
            string requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);

            var httpResponse = await _httpClient.PostAsync("/Template/ResetTemplate", new StringContent(requestJson, Encoding.UTF8, "application/json"));
            
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }
        }

        /// <summary>
        /// Deletes the signature template and it's data.
        /// </summary>
        /// <param name="request"><seealso cref="DeleteTemplateRequest"/></param>
        /// <returns>HTTP Object Response.</returns>
        public virtual async Task DeleteTemplateAsync(DeleteTemplateRequest request)
        {
            if (request.ClientName == null) request.ClientName = _gsvClientConfiguration.ClientName;
            string requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);

            var httpResponse = await _httpClient.PostAsync("/Template/DeleteTemplate", new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }
        }

        

        public virtual async Task<GetTemplateListResponse> GetTemplateListAsync(GetTemplateListRequest request)
        {
            if (request.ClientName == null) request.ClientName = _gsvClientConfiguration.ClientName;
            string requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);

            var httpResponse = await _httpClient.PostAsync("/Template/GetTemplateList", new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }

            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetTemplateListResponse>(responseJson, _jsonSerializerOptions);

            return result;
        }

        public virtual async Task<GetTemplateStatusResponse> GetTemplateStatusAsync(GetTemplateStatusRequest request)
        {
            if (request.ClientName == null) request.ClientName = _gsvClientConfiguration.ClientName;
            string requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);
            var httpResponse = await _httpClient.PostAsync("/Template/GetTemplateStatus", new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }

            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetTemplateStatusResponse>(responseJson, _jsonSerializerOptions);

            return result;
        }


        public virtual async Task<GetClientTemplateOptionsResponse> GetClientTemplateOptionsAsync(GetClientTemplateOptionsRequest request)
        {
            if (request.ClientName == null) request.ClientName = _gsvClientConfiguration.ClientName;
            string requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);
            var httpResponse = await _httpClient.PostAsync("/Client/GetTemplateOptions", new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }

            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetClientTemplateOptionsResponse>(responseJson, _jsonSerializerOptions);

            return result;
        }

        public virtual async Task UpdateClientTemplateOptionsAsync(UpdateClientTemplateOptionsRequest request)
        {
            if (request.ClientName == null) request.ClientName = _gsvClientConfiguration.ClientName;
            string requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);
            var httpResponse = await _httpClient.PostAsync("/Client/UpdateTemplateOptions", new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }
        }

        public virtual async Task<VerificationGetVersionResponse> VerificationGetVersionAsync()
        {
            var httpResponse = await _httpClient.PostAsync("/Verification/GetVersion", new StringContent("", Encoding.UTF8, "application/json"));
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }

            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<VerificationGetVersionResponse>(responseJson, _jsonSerializerOptions);

            return result;
        }

        /// <summary>
        /// Verifies a signature agains a signature template on the server.
        /// </summary>
        /// <param name="request"><seealso cref="VerifySignatureRequest"/></param>
        /// <returns>The signature verification result.</returns>
        public virtual async Task<VerifySignatureResponse> VerifySignatureAsync(VerifySignatureRequest request)
        {
            if (request.ClientName == null) request.ClientName = _gsvClientConfiguration.ClientName;
            string requestJson = JsonSerializer.Serialize(request, _jsonSerializerOptions);

            var httpResponse = await _httpClient.PostAsync("/Verification/VerifySignature", new StringContent(requestJson, Encoding.UTF8, "application/json"));

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }

            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<VerifySignatureResponse>(responseJson, _jsonSerializerOptions);

            return result;
        }
                
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _httpClient?.Dispose();
                    _httpClient = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GsvClientBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
