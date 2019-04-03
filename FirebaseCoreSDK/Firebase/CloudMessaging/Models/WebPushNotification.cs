namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using System.Collections.Generic;

    using FirebaseCoreSDK.Exceptions;

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents the Webpush-specific notification options that can be included in a
    ///     <see cref="FirebasePushMessage" />. Supports most standard options defined in the
    ///     <see href="https://developer.mozilla.org/en-US/docs/Web/API/notification/Notification">
    ///         Web Notification specification
    ///     </see>
    ///     .
    /// </summary>
    public sealed class WebpushNotification
    {
        /// <summary>
        ///     Gets or sets a collection of Webpush notification actions.
        /// </summary>
        [JsonProperty("actions")]
        public IEnumerable<Action> Actions { get; set; }

        /// <summary>
        ///     Gets or sets the URL of the image used to represent the notification when there is not
        ///     enough space to display the notification itself.
        /// </summary>
        [JsonProperty("badge")]
        public string Badge { get; set; }

        /// <summary>
        ///     Gets or sets the body text of the notification.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        ///     Gets or sets the custom key-value pairs that will be included in the
        ///     notification. This is exposed as an <see cref="IDictionary{TKey, TValue}" /> to support
        ///     correct deserialization of custom properties.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> CustomData { get; set; }

        /// <summary>
        ///     Gets or sets some arbitrary data that will be included in the notification.
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }

        /// <summary>
        ///     Gets or sets the direction in which to display the notification.
        /// </summary>
        [JsonIgnore]
        public Direction? Direction { get; set; }

        /// <summary>
        ///     Gets or sets the URL to the icon of the notification.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        ///     Gets or sets the URL of an image to be displayed in the notification.
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        ///     Gets or sets the language of the notification.
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; set; }

        /// <summary>
        ///     Gets or sets whether the user should be notified after a new notification replaces an
        ///     old one.
        /// </summary>
        [JsonProperty("renotify")]
        public bool? Renotify { get; set; }

        /// <summary>
        ///     Gets or sets whether the notification should remain active until the user clicks or
        ///     dismisses it, rather than closing it automatically.
        /// </summary>
        [JsonProperty("requireInteraction")]
        public bool? RequireInteraction { get; set; }

        /// <summary>
        ///     Gets or sets whether the notification should be silent.
        /// </summary>
        [JsonProperty("silent")]
        public bool? Silent { get; set; }

        /// <summary>
        ///     Gets or sets an identifying tag for the notification.
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }

        /// <summary>
        ///     Gets or sets the notification's timestamp value in milliseconds.
        /// </summary>
        [JsonProperty("timestamp")]
        public long? TimestampMillis { get; set; }

        /// <summary>
        ///     Gets or sets the title text of the notification.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets a vibration pattern for the receiving device's vibration hardware.
        /// </summary>
        [JsonProperty("vibrate")]
        public int[] Vibrate { get; set; }

        /// <summary>
        ///     Gets or sets the string representation of the <see cref="Direction" /> property.
        /// </summary>
        [JsonProperty("dir")]
        private string DirectionString
        {
            get
            {
                switch (Direction)
                {
                    case Models.Direction.Auto:
                        return "auto";
                    case Models.Direction.LeftToRight:
                        return "ltr";
                    case Models.Direction.RightToLeft:
                        return "rtl";
                    default:
                        return null;
                }
            }
            set
            {
                switch (value)
                {
                    case "auto":
                        Direction = Models.Direction.Auto;
                        return;
                    case "ltr":
                        Direction = Models.Direction.LeftToRight;
                        return;
                    case "rtl":
                        Direction = Models.Direction.RightToLeft;
                        return;
                    default:
                        throw new FirebaseException($"Invalid direction value: {value}. Only 'auto', 'rtl' and 'ltr' " + "are allowed.");
                }
            }
        }
    }
}