namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    public class PushMessageResponse
    {
        [JsonProperty("status_code")]
        public string ApnsStatusCode { get; set; }

        [JsonProperty("reason")]
        public string ApnsStatusReason { get; set; }

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}