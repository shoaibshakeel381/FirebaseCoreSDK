namespace FirebaseCoreSDK.Firebase.Storage.Models
{
    #region Namespace Imports

    using System;

    using FirebaseCoreAdmin.Firebase.Storage;

    #endregion


    public class SigningOption
    {
        public SigningAction Action { get; set; }
        public string ContentMD5 { get; set; }
        public string ContentType { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Path { get; set; }
    }
}