namespace FirebaseCoreSDK.Firebase.Storage.Models
{
    using System;

    public class ObjectMetadata
    {
        public string ContentType { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Md5Hash { get; set; }
        public long Size { get; set; }
    }
}
