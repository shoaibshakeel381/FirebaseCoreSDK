
namespace FirebaseCoreSDK.HttpClients.Auth.Serializer
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Serialization;

    public class AccessTokenResolver : DefaultContractResolver
    {
        private static readonly Dictionary<string, string> PropertyMappings = new Dictionary<string, string>
        {
            { "AccessToken","access_token"},
            { "TokenType", "token_type"},
            { "ExpiresIn", "expires_in"}
        };

        protected override string ResolvePropertyName(string propertyName)
        {
            return !PropertyMappings.TryGetValue(propertyName, out var resolvedName) ? base.ResolvePropertyName(propertyName) : resolvedName;
        }

    }
}
