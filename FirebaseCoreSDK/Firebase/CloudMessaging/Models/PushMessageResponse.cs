using Newtonsoft.Json;

namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    public class PushMessageResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("status_code")]
        public string ApnsStatusCode { get; set; }

        [JsonProperty("reason")]
        public string ApnsStatusReason { get; set; }
    }
}
