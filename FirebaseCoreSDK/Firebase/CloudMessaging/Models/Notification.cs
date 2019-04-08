namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents the notification parameters that can be included in a <see cref="FirebasePushMessage" />.
    /// </summary>
    public sealed class Notification
    {
        /// <summary>
        ///     Gets or sets the body of the notification.
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        /// <summary>
        ///     Gets or sets the title of the notification.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}