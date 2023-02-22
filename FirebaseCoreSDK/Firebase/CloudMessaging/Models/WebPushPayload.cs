namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents the Webpush protocol options that can be included in a <see cref="FirebasePushMessage" />.
    /// </summary>
    public sealed class WebPushPayload
    {
        /// <summary>
        ///     Gets or sets the Webpush data fields. When set, overrides any data fields set via
        ///     <see cref="FirebasePushMessage.Data" />.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public IReadOnlyDictionary<string, string> Data { get; set; }

        /// <summary>
        ///     Options for features provided by the FCM SDK for Web.
        /// </summary>
        [JsonProperty(PropertyName = "fcm_options", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public WebPushFcmOptions FcmOptions { get; set; }

        /// <summary>
        ///     Gets or sets the Webpush HTTP headers. Refer
        ///     <see href="https://tools.ietf.org/html/rfc8030#section-5">
        ///         Webpush specification
        ///     </see>
        ///     for supported headers.
        /// </summary>
        [JsonProperty(PropertyName = "headers")]
        public IReadOnlyDictionary<string, string> Headers { get; set; }

        /// <summary>
        ///     Gets or sets the Webpush notification that will be included in the message.
        /// </summary>
        [JsonProperty(PropertyName = "notification")]
        public WebPushNotification Notification { get; set; }
    }
}