using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interface;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FreeCourse.Web.Services.Abstract
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _settings;
        private readonly ClientSettings _clientSettings;
        private readonly IClientAccessTokenCache _cache;
        private readonly HttpClient _httpClient;
        public ClientCredentialTokenService(IOptions<ServiceApiSettings> settings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache cache,HttpClient httpClient)
        {
            _settings = settings.Value;
            _clientSettings = clientSettings.Value;
            _cache = cache;
            _httpClient = httpClient;
        }

        public async Task<string> GetToken()
        {
            var currenttoken = await _cache.GetAsync("WebClientToken");
            if(currenttoken!=null)
            {
                return currenttoken.AccessToken;
            }
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _settings.IdentityBaseURL,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });
            if (disco.IsError)
            {
                throw disco.Exception;
            }
            var clientcredentialtokenrequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.WebMvcClient.ClientId,
                ClientSecret = _clientSettings.WebMvcClient.ClientSecret,
                Address=disco.TokenEndpoint
            };

            var newtoken = await _httpClient.RequestClientCredentialsTokenAsync(clientcredentialtokenrequest);
            if (newtoken.IsError)
            {
                throw newtoken.Exception;
            }

            await _cache.SetAsync("WebClientToken", newtoken.AccessToken, newtoken.ExpiresIn);
            return newtoken.AccessToken;
        }
    }
}
