namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Options for features provided by the FCM SDK for iOS.
    /// </summary>
    public sealed class ApnsFcmOptions
    {
        /// <summary>
        ///     Contains the URL of an image that is going to be displayed in a notification. If present, it will override
        ///     <see cref="Notification.Image" />
        /// </summary>
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
    }
}