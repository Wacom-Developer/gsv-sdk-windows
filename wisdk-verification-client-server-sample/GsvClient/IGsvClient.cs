using GsvClient.Models;
using System;
using System.Threading.Tasks;

namespace GsvClient
{
    /// <summary>
    /// A basic web interface for signature verification
    /// </summary>
    public interface IGsvClient : IDisposable
    {
        /// <summary>
        /// Returns the address of the server is the client is connected to.
        /// </summary>        
        string GetServerAddress();

        /// <summary>
        /// Retrieves the underlying Wacom Verification component version
        /// </summary>
        /// <returns>The COM component version string</returns>
        Task<VerificationGetVersionResponse> VerificationGetVersionAsync();

        /// <summary>
        /// Creates a new client
        /// </summary>
        /// <param name="request">Provides the client name and optional default template options</param>
        /// <returns>void</returns>
        Task CreateClientAsync(CreateClientRequest request);

        /// <summary>
        /// Retrieves the client's default template options for creating new templates.
        /// </summary>
        /// <param name="request">The client to query</param>
        /// <returns>The template options</returns>
        Task<GetClientTemplateOptionsResponse> GetClientTemplateOptionsAsync(GetClientTemplateOptionsRequest request);

        /// <summary>
        /// Updates the client's default template options for creating new templates with.
        /// </summary>
        /// <param name="request">The new options to use</param>
        /// <returns>void</returns>
        Task UpdateClientTemplateOptionsAsync(UpdateClientTemplateOptionsRequest request);

        /// <summary>
        /// Creates a new template
        /// </summary>
        /// <param name="request">Provides the template name to use</param>
        /// <returns>void</returns>
        Task CreateTemplateAsync(CreateTemplateRequest request);

        /// <summary>
        /// Retrieves the current status of a named template
        /// </summary>
        /// <param name="request">The name of the template to query</param>
        /// <returns>The current status of the template</returns>
        Task<GetTemplateStatusResponse> GetTemplateStatusAsync(GetTemplateStatusRequest request);

        /// <summary>
        /// Deletes a named template
        /// </summary>
        /// <remarks>Only the client that created a template can delete it.</remarks>
        /// <param name="request">The name of the template to delete</param>
        /// <returns>void</returns>
        Task DeleteTemplateAsync(DeleteTemplateRequest request);

        /// <summary>
        /// Resets and empties the template.
        /// </summary>
        /// <remarks>Only the client that created the template can reset it. Resetting a template inherits the client's current template options.</remarks>
        /// <param name="request">The name of the template to reset</param>
        /// <returns>void</returns>
        Task ResetTemplateAsync(ResetTemplateRequest request);


        /// <summary>
        /// Retrieves a list of all the templates in the repository.
        /// </summary>
        /// <param name="request">reserved for future use. (Currently contains the client name)</param>
        /// <remarks>For a real implementation, this should introduce paging and limits to the amount returned in a single request.</remarks>
        /// <returns>The list of all templates</returns>
        Task<GetTemplateListResponse> GetTemplateListAsync(GetTemplateListRequest request);

        /// <summary>
        /// Verifies a signature against a template.
        /// </summary>
        /// <param name="request">Provides the name of the template and the signature to verify.</param>
        /// <returns>The verification result.</returns>
        Task<VerifySignatureResponse> VerifySignatureAsync(VerifySignatureRequest request);
    }
}
