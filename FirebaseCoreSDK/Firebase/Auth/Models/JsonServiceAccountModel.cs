namespace FirebaseCoreSDK.Firebase.Auth.Models
{
    using Newtonsoft.Json;

    public class JsonServiceAccountModel
    {
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("private_key_id")]
        public string PrivateKeyId { get; set; }

        [JsonProperty("private_key")]
        public string PrivateKey { get; set; }

        [JsonProperty("client_email")]
        public string ClientEmail { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("auth_uri")]
        public string AuthUri { get; set; }

        [JsonProperty("token_uri")]
        public string TokenUri { get; set; }
    }
}
