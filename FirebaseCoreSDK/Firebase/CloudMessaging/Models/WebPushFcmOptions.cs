namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Options for features provided by the FCM SDK for Web.
    /// </summary>
    public sealed class WebPushFcmOptions
    {
        /// <summary>
        ///     The link to open when the user clicks on the notification. For all URL values, HTTPS is required.
        /// </summary>
        [JsonProperty(PropertyName = "link", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Link { get; set; }
    }
}