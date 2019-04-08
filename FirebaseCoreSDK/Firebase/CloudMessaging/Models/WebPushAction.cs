namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents an action available to users when the notification is presented.
    /// </summary>
    public sealed class WebPushAction
    {
        /// <summary>
        ///     Gets or sets the name of the WebPushAction.
        /// </summary>
        [JsonProperty("action")]
        public string ActionName { get; set; }

        /// <summary>
        ///     Gets or sets the icon URL.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        ///     Gets or sets the title text.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}