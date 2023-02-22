namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents the APNS-specific options that can be included in a <see cref="FirebasePushMessage" />. Refer
    ///     to
    ///     <see
    ///         href="https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/APNSOverview.html">
    ///         APNs documentation
    ///     </see>
    ///     for various headers and payload fields supported by APNS.
    /// </summary>
    public sealed class Apns
    {
        /// <summary>
        ///     Gets or sets a collapse key for the message. Collapse key serves as an identifier for a
        ///     group of messages that can be collapsed, so that only the last message gets sent when
        ///     delivery can be resumed. A maximum of 4 different collapse keys may be active at any
        ///     given time.
        /// </summary>
        [JsonIgnore]
        public string CollapseKey { get; set; }

        /// <summary>
        ///     Options for features provided by the FCM SDK for IOS.
        /// </summary>
        [JsonProperty(PropertyName = "fcm_options", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ApnsFcmOptions FcmOptions { get; set; }

        /// <summary>
        ///     Notification Payload
        /// </summary>
        [JsonProperty(PropertyName = "payload")]
        public ApnsPayload Payload { get; set; }

        /// <summary>
        ///     Gets or sets the priority of the message.
        /// </summary>
        [JsonIgnore]
        public Priority? Priority { get; set; }

        /// <summary>
        ///     Gets or sets the time-to-live duration of the message.
        /// </summary>
        [JsonIgnore]
        public TimeSpan? TimeToLive { get; set; }

        /// <summary>
        ///     Gets or sets the APNs headers.
        /// </summary>
        [JsonProperty(PropertyName = "headers")]
        private IReadOnlyDictionary<string, string> Headers
        {
            get
            {
                var headers = new Dictionary<string, string>();

                if (!string.IsNullOrWhiteSpace(CollapseKey))
                {
                    headers.Add("apns-collapse-id", CollapseKey);
                }

                if (Priority != null)
                {
                    headers.Add("apns-priority", Priority.Value == Models.Priority.High ? "10" : "5");
                }

                // ReSharper disable once InvertIf
                if (TimeToLive != null)
                {
                    var ttl = (long)DateTime.UtcNow.AddSeconds(TimeToLive.Value.TotalSeconds).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    headers.Add("apns-expiration", ttl.ToString());
                }

                return headers;
            }
        }
    }
}