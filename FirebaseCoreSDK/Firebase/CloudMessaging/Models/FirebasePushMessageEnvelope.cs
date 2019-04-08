namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    public sealed class FirebasePushMessageEnvelope
    {
        [JsonProperty("validate_only")]
        public bool DryRun { get; set; }

        [JsonProperty("message")]
        public FirebasePushMessage Message { get; set; }
    }
}