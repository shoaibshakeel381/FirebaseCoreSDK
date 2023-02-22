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
        ///     Contains the URL of an image that is going to be downloaded on the device and displayed in a notification. <br />
        ///     JPEG, PNG, BMP have full support across platforms. Animated GIF and video only work on iOS. <br />WebP and HEIF
        ///     have varying levels of support across platforms and platform versions. <br/>Android has 1MB image size limit.
        /// </summary>
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        /// <summary>
        ///     Gets or sets the title of the notification.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}