namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    public sealed class PushMessageResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}