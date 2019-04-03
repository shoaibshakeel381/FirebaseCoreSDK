namespace FirebaseCoreSDK.Firebase.Database
{
    #region Namespace Imports

    using Newtonsoft.Json;

    #endregion


    public class KeyEntity
    {
        [JsonIgnore]
        public string Key { get; set; }
    }
}