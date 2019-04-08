namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    #region Namespace Imports

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
    public sealed class ApnsPayload
    {
        [JsonProperty("aps")]
        public ApnsAps Aps { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> CustomData { get; set; }
    }
}