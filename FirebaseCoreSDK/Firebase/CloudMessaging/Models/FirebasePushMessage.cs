namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents a message that can be sent via Firebase Cloud Messaging (FCM). Contains payload
    ///     information as well as the recipient information. The recipient information must be
    ///     specified by setting exactly one of the <see cref="Token" />, <see cref="Topic" /> or
    ///     <see cref="Condition" /> fields.
    /// </summary>
    public sealed class FirebasePushMessage
    {
        /// <summary>
        ///     Gets or sets the Android-specific information to be included in the message.
        /// </summary>
        [JsonProperty(PropertyName = "android")]
        public Android Android { get; set; }

        /// <summary>
        ///     Gets or sets the APNs-specific information to be included in the message.
        /// </summary>
        [JsonProperty(PropertyName = "apns")]
        public Apns Apns { get; set; }

        /// <summary>
        ///     Gets or sets the FCM condition to which the message should be sent. Must be a valid
        ///     condition string such as <c>"'foo' in topics"</c>.
        /// </summary>
        [JsonProperty(PropertyName = "condition")]
        public string Condition { get; set; }

        /// <summary>
        ///     Gets or sets a collection of key-value pairs that will be added to the message as data
        ///     fields. Keys and the values must not be null.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public IReadOnlyDictionary<string, string> Data { get; set; }

        /// <summary>
        ///     Gets or sets the notification information to be included in the message.
        /// </summary>
        [JsonProperty(PropertyName = "notification")]
        public Notification Notification { get; set; }

        /// <summary>
        ///     Gets or sets the registration token of the device to which the message should be sent.
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        /// <summary>
        ///     Gets or sets the name of the FCM topic to which the message should be sent. Topic names
        ///     should not contain the <c>/topics/</c> prefix.
        /// </summary>
        [JsonProperty(PropertyName = "topic")]
        public string Topic { get; set; }

        /// <summary>
        ///     Gets or sets the Webpush-specific information to be included in the message.
        /// </summary>
        [JsonProperty(PropertyName = "webpush")]
        public WebPushPayload WebPush { get; set; }
    }
}