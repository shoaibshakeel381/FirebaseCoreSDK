namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    /// <summary>
    ///     The sound configuration for APNs critical alerts.
    /// </summary>
    public sealed class ApnsSound
    {
        /// <summary>
        ///     Gets or sets a value indicating whether to set the critical alert flag on the sound
        ///     configuration.
        /// </summary>
        [JsonIgnore]
        public bool Critical { get; set; }

        /// <summary>
        ///     Gets or sets the name of the sound to be played. This should be a sound file in your
        ///     app's main bundle or in the <c>Library/Sounds</c> folder of your app's container
        ///     directory. Specify the string <c>default</c> to play the system sound.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the volume for the critical alert's sound. Must be a value between 0.0
        ///     (silent) and 1.0 (full volume).
        /// </summary>
        [JsonProperty("volume")]
        public double? Volume { get; set; }

        /// <summary>
        ///     Gets or sets the integer representation of the <see cref="Critical" /> property, which
        ///     is how APNs expects it.
        /// </summary>
        [JsonProperty("critical")]
        private int? CriticalInt
        {
            get
            {
                if (Critical)
                {
                    return 1;
                }

                return null;
            }
        }
    }
}