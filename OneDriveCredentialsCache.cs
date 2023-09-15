using Azure.Identity;
using System.IO;
using Tizen.Security.SecureRepository;

namespace OneDrivePhotos
{
    internal class OneDriveCredentialsCache
    {
        private const string CredentialsCacheKey = "OneDriveCredentials";
        private const string CredentialsCachePwd = "OneDriveCredentialsPwd";

        public static AuthenticationRecord Get()
        {
            try
            {
                var serializedAuthRecord = DataManager.Get(CredentialsCacheKey, CredentialsCachePwd);
                using var credsStream = new MemoryStream(serializedAuthRecord);
                return AuthenticationRecord.Deserialize(credsStream);
            }
            catch
            {
                return null;
            }
        }

        public static void Set(AuthenticationRecord authRecord)
        {
            try
            {
                DataManager.RemoveAlias(CredentialsCacheKey);
            }
            catch
            {
            }
            using var credsStream = new MemoryStream();
            authRecord.Serialize(credsStream);
            DataManager.Save(CredentialsCacheKey, credsStream.ToArray(), new Policy(CredentialsCachePwd, true));
        }
    }
}
