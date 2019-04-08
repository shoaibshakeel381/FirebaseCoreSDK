namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents the Android-specific options that can be included in a <see cref="FirebasePushMessage" />.
    /// </summary>
    public sealed class Android
    {
        /// <summary>
        ///     Gets or sets the priority of the message.
        /// </summary>
        [JsonIgnore]
        public Priority? Priority { get; set; }

        /// <summary>
        ///     Gets or sets a collapse key for the message. Collapse key serves as an identifier for a
        ///     group of messages that can be collapsed, so that only the last message gets sent when
        ///     delivery can be resumed. A maximum of 4 different collapse keys may be active at any
        ///     given time.
        /// </summary>
        [JsonProperty(PropertyName = "collapse_key")]
        public string CollapseKey { get; set; }

        /// <summary>
        ///     Gets or sets a collection of key-value pairs that will be added to the message as data
        ///     fields. Keys and the values must not be null. When set, overrides any data fields set
        ///     on the top-level
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public IReadOnlyDictionary<string, string> Data { get; set; }

        /// <summary>
        ///     Gets or sets the Android notification to be included in the message.
        /// </summary>
        [JsonProperty(PropertyName = "notification")]
        public AndroidNotification Notification { get; set; }

        /// <summary>
        ///     Gets or sets the package name of the application where the registration tokens must
        ///     match in order to receive the message.
        /// </summary>
        [JsonProperty(PropertyName = "restricted_package_name")]
        public string RestrictedPackageName { get; set; }

        /// <summary>
        ///     Gets or sets the time-to-live duration of the message.
        /// </summary>
        [JsonIgnore]
        public TimeSpan? TimeToLive { get; set; }

        /// <summary>
        ///     Gets or sets the string representation of <see cref="Priority" /> as accepted by the FCM
        ///     backend service.
        /// </summary>
        [JsonProperty(PropertyName = "priority")]
        private string PriorityString {
            get
            {
                if (Priority == null)
                {
                    return null;
                }

                return Priority.Value == Models.Priority.High ? "high" : "normal";
            }
        }

        /// <summary>
        ///     Gets or sets the string representation of <see cref="TimeToLive" /> as accepted by the
        ///     FCM backend service. The string ends in the suffix "s" (indicating seconds) and is
        ///     preceded by the number of seconds, with nanoseconds expressed as fractional seconds.
        /// </summary>
        [JsonProperty("ttl")]
        private string TimeToLiveString
        {
            get
            {
                if (TimeToLive == null)
                {
                    return null;
                }

                var totalSeconds = TimeToLive.Value.TotalSeconds;
                var seconds = (long)Math.Floor(totalSeconds);
                var subsecondNanos = (long)((totalSeconds - seconds) * 1e9);

                return subsecondNanos > 0 ? $"{seconds}.{subsecondNanos:D9}s" : $"{seconds}s";
            }
        }
    }
}