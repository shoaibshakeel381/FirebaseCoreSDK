namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents an action available to users when the notification is presented.
    /// </summary>
    public sealed class Action
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Action" /> class.
        /// </summary>
        public Action() {}

        internal Action(Action action)
        {
            ActionName = action.ActionName;
            Title = action.Title;
            Icon = action.Icon;
        }

        /// <summary>
        ///     Gets or sets the name of the Action.
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