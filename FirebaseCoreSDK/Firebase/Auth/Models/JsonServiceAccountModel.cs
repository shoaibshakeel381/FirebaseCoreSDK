namespace FirebaseCoreSDK.Firebase.Auth.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    public class JsonServiceAccountModel
    {
        [JsonProperty("client_email")]
        public string ClientEmail { get; set; }

        [JsonProperty("private_key")]
        public string PrivateKey { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }
    }
}