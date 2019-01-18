namespace FirebaseCoreSDK.Firebase.CloudMessaging.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class FirebasePushMessage
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "topic", NullValueHandling = NullValueHandling.Ignore)]
        public string Topic { get; set; }

        [JsonProperty(PropertyName = "condition", NullValueHandling = NullValueHandling.Ignore)]
        public string Condition { get; set; }

        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Data { get; set; }

        [JsonProperty(PropertyName = "notification", NullValueHandling = NullValueHandling.Ignore)]
        public Notification Notification { get; set; }

        [JsonProperty(PropertyName = "android", NullValueHandling = NullValueHandling.Ignore)]
        public AndriodPayload Android { get; set; }

        [JsonProperty(PropertyName = "webpush", NullValueHandling = NullValueHandling.Ignore)]
        public WebPushPayload WebPush { get; set; }

        [JsonProperty(PropertyName = "apns", NullValueHandling = NullValueHandling.Ignore)]
        public ApnsPayload Apns { get; set; }
    }

    public class Notification
    {
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }
    }

    public class AndriodPayload
    {
        [JsonProperty(PropertyName = "collapse_key", NullValueHandling = NullValueHandling.Ignore)]
        public string CollapseKey { get; set; }
        
        [JsonProperty(PropertyName = "priority", NullValueHandling = NullValueHandling.Ignore)]
        public string Priority { get; set; }

        [JsonProperty(PropertyName = "ttl", NullValueHandling = NullValueHandling.Ignore)]
        public string Ttl { get; set; }

        [JsonProperty(PropertyName = "restricted_package_name", NullValueHandling = NullValueHandling.Ignore)]
        public string RestrictedPackageName { get; set; }

        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Data { get; set; }

        [JsonProperty(PropertyName = "notification", NullValueHandling = NullValueHandling.Ignore)]
        public AndroidNotification Notification { get; set; }
    }

    public class AndroidNotification
    {
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "sound", NullValueHandling = NullValueHandling.Ignore)]
        public string Sound { get; set; }

        [JsonProperty(PropertyName = "tag", NullValueHandling = NullValueHandling.Ignore)]
        public string Tag { get; set; }

        [JsonProperty(PropertyName = "click_action", NullValueHandling = NullValueHandling.Ignore)]
        public string ClickAction { get; set; }

        [JsonProperty(PropertyName = "body_loc_key", NullValueHandling = NullValueHandling.Ignore)]
        public string BodyLocKey { get; set; }

        [JsonProperty(PropertyName = "body_loc_args", NullValueHandling = NullValueHandling.Ignore)]
        public string[] BodyLocArgs { get; set; }

        [JsonProperty(PropertyName = "title_loc_key", NullValueHandling = NullValueHandling.Ignore)]
        public string TitleLocKey { get; set; }

        [JsonProperty(PropertyName = "title_loc_args", NullValueHandling = NullValueHandling.Ignore)]
        public string[] TitleLocArgs { get; set; }

        [JsonProperty(PropertyName = "channel_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ChannelId { get; set; }
    }

    public class ApnsPayload
    {
        [JsonProperty(PropertyName = "payload", NullValueHandling = NullValueHandling.Ignore)]
        public JToken Payload { get; set; }

        [JsonProperty(PropertyName = "headers", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Headers { get; set; }
    }

    public class WebPushPayload
    {
        [JsonProperty(PropertyName = "payload", NullValueHandling = NullValueHandling.Ignore)]
        public JToken Payload { get; set; }

        [JsonProperty(PropertyName = "headers", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Headers { get; set; }

        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Data { get; set; }

        [JsonProperty(PropertyName = "notification", NullValueHandling = NullValueHandling.Ignore)]
        public object Notification { get; set; }

        [JsonProperty(PropertyName = "fcm_options", NullValueHandling = NullValueHandling.Ignore)]
        public WebPushFcmOptions FcmOptions { get; set; }
    }

    public class WebPushFcmOptions
    {
        [JsonProperty(PropertyName = "link", NullValueHandling = NullValueHandling.Ignore)]
        public string Link { get; set; }
    }
}
