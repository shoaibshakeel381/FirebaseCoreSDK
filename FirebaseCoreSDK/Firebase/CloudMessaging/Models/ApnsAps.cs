namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     Represents the
    ///     <see
    ///         href="https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/PayloadKeyReference.html">
    ///         aps dictionary
    ///     </see>
    ///     that is part of every APNs message.
    /// </summary>
    public sealed class ApnsAps
    {
        private int? _badge;
        private string _category;
        private string _soundString;
        private string _threadId;

        /// <summary>
        ///     Gets or sets an advanced alert configuration to be included in the message.
        ///     together.It is an error to set both <see cref="Alert" /> and <see cref="AlertString" />
        ///     properties together.
        /// </summary>
        [JsonIgnore]
        public ApsAlert Alert { get; set; }

        /// <summary>
        ///     Gets or sets the alert text to be included in the message. To specify a more advanced
        ///     alert configuration, use the <see cref="Alert" /> property instead. It is an error to
        ///     set both <see cref="Alert" /> and <see cref="AlertString" /> properties together.
        /// </summary>
        [JsonIgnore]
        public string AlertString { get; set; }

        /// <summary>
        ///     Gets or sets the badge to be displayed with the message. Set to 0 to remove the badge.
        ///     When not specified, the badge will remain unchanged.
        /// </summary>
        [JsonProperty("badge")]
        public int? Badge { get => _badge ?? 0; set => _badge = value; }

        /// <summary>
        ///     Gets or sets the type of the notification.
        /// </summary>
        [JsonProperty("click_action")]
        public string Category { get => _category ?? string.Empty; set => _category = value; }

        /// <summary>
        ///     Gets or sets a value indicating whether to configure a background update notification.
        /// </summary>
        [JsonIgnore]
        public bool ContentAvailable { get; set; }

        /// <summary>
        ///     Gets or sets a collection of arbitrary key-value data to be included in the <c>aps</c>
        ///     dictionary.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> CustomData { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to include the <c>mutable-content</c> property
        ///     in the message. When set, this property allows clients to modify the notification via
        ///     app extensions.
        /// </summary>
        [JsonIgnore]
        public bool MutableContent { get; set; }

        /// <summary>
        ///     Gets or sets the critical alert sound to be played with the message. It is an error to
        ///     set both <see cref="SoundString" /> and <see cref="ApnsSound" /> properties together.
        /// </summary>
        [JsonIgnore]
        public ApnsSound Sound { get; set; }

        /// <summary>
        ///     Gets or sets the name of a sound file in your app's main bundle or in the
        ///     <c>Library/Sounds</c> folder of your app's container directory. Specify the
        ///     string <c>default</c> to play the system sound. It is an error to set both
        ///     <see cref="SoundString" /> and <see cref="ApnsSound" /> properties together.
        /// </summary>
        [JsonIgnore]
        public string SoundString { get => _soundString ?? string.Empty; set => _soundString = value; }

        /// <summary>
        ///     Gets or sets the app-specific identifier for grouping notifications.
        /// </summary>
        [JsonProperty("thread_id")]
        public string ThreadId { get => _threadId ?? string.Empty; set => _threadId = value; }

        /// <summary>
        ///     Gets or sets the alert configuration of the <c>aps</c> dictionary. Read from either
        ///     <see cref="Alert" /> or <see cref="AlertString" /> property.
        /// </summary>
        [JsonProperty("alert")]
        private object AlertObject
        {
            get
            {
                if (AlertString == null)
                {
                    return Alert;
                }

                if (Alert != null)
                {
                    throw new ArgumentException("Multiple specifications for alert (Alert and AlertString");
                }

                return AlertString;
            }
        }

        /// <summary>
        ///     Gets or sets the integer representation of the <see cref="ContentAvailable" /> property,
        ///     which is how APNs expects it.
        /// </summary>
        [JsonProperty("content_available")]
        private int? ContentAvailableInt => ContentAvailable ? 1 : 0;

        /// <summary>
        ///     Gets or sets the integer representation of the <see cref="MutableContent" /> property,
        ///     which is how APNs expects it.
        /// </summary>
        [JsonProperty("mutable_content")]
        private int? MutableContentInt => MutableContent ? 1 : 0;

        /// <summary>
        ///     Gets or sets the sound configuration of the <c>aps</c> dictionary. Read from either
        ///     <see cref="Sound" /> or <see cref="ApnsSound" /> property.
        /// </summary>
        [JsonProperty("sound")]
        private object SoundObject
        {
            get
            {
                if (SoundString == null)
                {
                    return Sound;
                }

                if (Sound != null)
                {
                    throw new ArgumentException("Multiple specifications for sound (Sound and SoundString");
                }

                return SoundString;
            }
        }
    }
}