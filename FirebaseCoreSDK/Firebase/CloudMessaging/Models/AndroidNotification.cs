namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents the Android-specific notification options that can be included in a <see cref="FirebasePushMessage" />
    /// </summary>
    public sealed class AndroidNotification
    {
        /// <summary>
        ///     Gets or sets the title of the Android notification. When provided, overrides the title
        ///     set via <see cref="Notification.Body" />.
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        /// <summary>
        ///     Gets or sets the collection of resource key strings that will be used in place of the
        ///     format specifiers in <see cref="BodyLocKey" />.
        /// </summary>
        [JsonProperty(PropertyName = "body_loc_args")]
        public string[] BodyLocArgs { get; set; }

        /// <summary>
        ///     Gets or sets the key of the body string in the app's string resources to use to
        ///     localize the body text.
        /// </summary>
        [JsonProperty(PropertyName = "body_loc_key")]
        public string BodyLocKey { get; set; }

        /// <summary>
        ///     Gets or sets the Android notification channel ID (new in Android O). The app must
        ///     create a channel with this channel ID before any notification with this channel ID is
        ///     received. If you don't send this channel ID in the request, or if the channel ID
        ///     provided has not yet been created by the app, FCM uses the channel ID specified in the
        ///     app manifest.
        /// </summary>
        [JsonProperty(PropertyName = "channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        ///     Gets or sets the action associated with a user click on the notification. If specified,
        ///     an activity with a matching Intent Filter is launched when a user clicks on the
        ///     notification.
        /// </summary>
        [JsonProperty(PropertyName = "click_action")]
        public string ClickAction { get; set; }

        /// <summary>
        ///     Gets or sets the notification icon color. Must be of the form <c>#RRGGBB</c>.
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        /// <summary>
        ///     Gets or sets the icon of the Android notification.
        /// </summary>
        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        /// <summary>
        ///     Contains the URL of an image that is going to be displayed in a notification. If present, it will override image
        ///     set via <see cref="Notification.Image" />.
        /// </summary>
        [JsonProperty(PropertyName = "image", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Image { get; set; }

        /// <summary>
        ///     Set whether or not this notification is relevant only to the current device. Some notifications can be <br />
        ///     bridged to other devices for remote display, such as a Wear OS watch. This hint can be set to recommend this
        ///     notification not be bridged.
        /// </summary>
        [JsonProperty(PropertyName = "local_only", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? LocalOnly { get; set; }

        /// <summary>
        ///     Gets or sets the sound to be played when the device receives the notification.
        /// </summary>
        [JsonProperty(PropertyName = "sound")]
        public string Sound { get; set; }

        /// <summary>
        ///     When set to false or unset, the notification is automatically dismissed when the user clicks it in the panel.
        ///     <br />When set to true, the notification persists even when the user clicks it.
        /// </summary>
        [JsonProperty(PropertyName = "sticky", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Sticky { get; set; }

        /// <summary>
        ///     Gets or sets the notification tag. This is an identifier used to replace existing
        ///     notifications in the notification drawer. If not specified, each request creates a new
        ///     notification.
        /// </summary>
        [JsonProperty(PropertyName = "tag", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Tag { get; set; }

        /// <summary>
        ///     Sets the "ticker" text, which is sent to accessibility services. <br />Prior to API level 21 (Lollipop), sets the
        ///     text that is displayed in the status bar when the notification first arrives.
        /// </summary>
        [JsonProperty(PropertyName = "ticker", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Ticker { get; set; }

        /// <summary>
        ///     Gets or sets the title of the Android notification. When provided, overrides the title
        ///     set via <see cref="Notification.Title" />.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the collection of resource key strings that will be used in place of the
        ///     format specifiers in <see cref="TitleLocKey" />.
        /// </summary>
        [JsonProperty(PropertyName = "title_loc_args")]
        public string[] TitleLocArgs { get; set; }

        /// <summary>
        ///     Gets or sets the key of the title string in the app's string resources to use to
        ///     localize the title text.
        /// </summary>
        [JsonProperty(PropertyName = "title_loc_key")]
        public string TitleLocKey { get; set; }
    }
}