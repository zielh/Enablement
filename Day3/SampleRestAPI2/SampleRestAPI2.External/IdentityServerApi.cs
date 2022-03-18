using IdentityModel.Client;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;

namespace SampleRestAPI2Auth.External
{
    public class IdentityServerApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IClientStore _clientStore;

        public IdentityServerApi(IHttpClientFactory httpClientFactory, IConfiguration config, IClientStore clientStore)
        {
            _httpClient = httpClientFactory.CreateClient();
            _config = config;
            _clientStore = clientStore;
        }


        public async Task<TokenResponse> AuthWithIdentityServer(string userName, string password, bool bypassPassword)
        {
            DiscoveryDocumentRequest discoReq = new DiscoveryDocumentRequest()
            {
                Address = _config.GetSection("AuthorizationServer").GetSection("Address").Value,
                Policy = new DiscoveryPolicy()
                {
                    RequireHttps = false,
                    ValidateEndpoints = false,
                    ValidateIssuerName = false
                }
            };

            DiscoveryDocumentResponse discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(discoReq);


            Client client = await _clientStore.FindClientByIdAsync(_config.GetSection("Service").GetSection("ClientId").Value);

            PasswordTokenRequest passwordTokenRequest = new PasswordTokenRequest()
            {

                Address = discoveryDocument.TokenEndpoint,
                ClientId = _config.GetSection("Service").GetSection("ClientId").Value,
                ClientSecret = _config.GetSection("Service").GetSection("ClientSecret").Value,
                GrantType = GrantType.ResourceOwnerPassword,
                Scope = client.AllowedScopes.Aggregate((p, n) => p + " " + n),
                UserName = userName,
                Password = password
            };

            if (bypassPassword)
            {
                passwordTokenRequest.Parameters.Add("bypassPassword", "true");
            }


            TokenResponse tokenResponse = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);
            return tokenResponse;
        }
    }
}
