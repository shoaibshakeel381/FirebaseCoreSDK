namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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
    public class ApnsPayload
    {
        /// <summary>
        ///     Gets or sets the APNs headers.
        /// </summary>
        [JsonProperty(PropertyName = "headers")]
        public IReadOnlyDictionary<string, string> Headers { get; set; }

        [JsonProperty(PropertyName = "payload")]
        private JToken Payload { get; set; }
    }
}