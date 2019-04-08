namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents the
    ///     <see
    ///         href="https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/PayloadKeyReference.html#//apple_ref/doc/uid/TP40008194-CH17-SW5">
    ///         alert property
    ///     </see>
    ///     that can be included in the <c>aps</c> dictionary of an APNs
    ///     payload.
    /// </summary>
    public sealed class ApsAlert
    {
        private string _actionLocKey;
        private string _launchImage;
        private string _locKey;
        private string _subtitle;
        private string _subtitleLocKey;
        private string _title;
        private string _titleLocKey;

        /// <summary>
        ///     Gets or sets the key of the text in the app's string resources to use to localize the
        ///     action button text.
        /// </summary>
        [JsonProperty("action-loc-key")]
        public string ActionLocKey { get => _actionLocKey ?? string.Empty; set => _actionLocKey = value; }

        /// <summary>
        ///     Gets or sets the body of the alert. When provided, overrides the body set via
        ///     <see cref="Notification.Body" />.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        ///     Gets or sets the launch image for the notification action.
        /// </summary>
        [JsonProperty("launch-image")]
        public string LaunchImage { get => _launchImage ?? string.Empty; set => _launchImage = value; }

        /// <summary>
        ///     Gets or sets the resource key strings that will be used in place of the format
        ///     specifiers in <see cref="LocKey" />.
        /// </summary>
        [JsonProperty("loc-args")]
        public IEnumerable<string> LocArgs { get; set; }

        /// <summary>
        ///     Gets or sets the key of the body string in the app's string resources to use to
        ///     localize the body text.
        /// </summary>
        [JsonProperty("loc-key")]
        public string LocKey { get => _locKey ?? string.Empty; set => _locKey = value; }

        /// <summary>
        ///     Gets or sets the subtitle of the alert.
        /// </summary>
        [JsonProperty("subtitle")]
        public string Subtitle { get => _subtitle ?? string.Empty; set => _subtitle = value; }

        /// <summary>
        ///     Gets or sets the resource key strings that will be used in place of the format
        ///     specifiers in <see cref="SubtitleLocKey" />.
        /// </summary>
        [JsonProperty("subtitle-loc-args")]
        public IEnumerable<string> SubtitleLocArgs { get; set; }

        /// <summary>
        ///     Gets or sets the key of the subtitle string in the app's string resources to use to
        ///     localize the subtitle text.
        /// </summary>
        [JsonProperty("subtitle-loc-key")]
        public string SubtitleLocKey { get => _subtitleLocKey ?? string.Empty; set => _subtitleLocKey = value; }

        /// <summary>
        ///     Gets or sets the title of the alert. When provided, overrides the title set via
        ///     <see cref="Notification.Title" />.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get => _title ?? string.Empty; set => _title = value; }

        /// <summary>
        ///     Gets or sets the resource key strings that will be used in place of the format
        ///     specifiers in <see cref="TitleLocKey" />.
        /// </summary>
        [JsonProperty("title-loc-args")]
        public IEnumerable<string> TitleLocArgs { get; set; }

        /// <summary>
        ///     Gets or sets the key of the title string in the app's string resources to use to
        ///     localize the title text.
        /// </summary>
        [JsonProperty("title-loc-key")]
        public string TitleLocKey { get => _titleLocKey ?? string.Empty; set => _titleLocKey = value; }
    }
}