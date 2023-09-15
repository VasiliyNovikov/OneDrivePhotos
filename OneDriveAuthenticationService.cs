using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using System;
using System.Threading.Tasks;

namespace OneDrivePhotos
{
    internal static class OneDriveAuthenticationService
    {
        private const string ClientId = "dcae5fa6-d6d2-4a97-b53c-d1e4cce8a351";
        private const string TenantId = "consumers";
        private const string Scope = "Files.Read";

        private const string CredentialsCacheName = "OneDriveCredentials";

        public static async Task<OneDriveService> Authenticate(Action<DeviceCodeInfo> authenticationCallback)
        {
            var options = new DeviceCodeCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                ClientId = ClientId,
                TenantId = TenantId,
                AuthenticationRecord = OneDriveCredentialsCache.Get(),
                TokenCachePersistenceOptions = new TokenCachePersistenceOptions
                {
                    Name = CredentialsCacheName,
                    UnsafeAllowUnencryptedStorage = true,
                },
                DeviceCodeCallback = (codeInfo, cancellation) =>
                {
                    authenticationCallback(codeInfo);
                    return Task.CompletedTask;
                },
            };

            var credential = new DeviceCodeCredential(options);

            if (options.AuthenticationRecord == null)
            {
                var authRecord = await credential.AuthenticateAsync(new TokenRequestContext(new[] { Scope }));
                OneDriveCredentialsCache.Set(authRecord);
            }

            var graphClient = new GraphServiceClient(credential, new[] { Scope });
            var drive = await graphClient.Me.Drive.GetAsync();

            return new OneDriveService(drive);
        }
    }
}
