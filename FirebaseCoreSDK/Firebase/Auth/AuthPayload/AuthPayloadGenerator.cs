namespace FirebaseCoreSDK.Firebase.Auth.AuthPayload
{
    #region Namespace Imports

    using System.Collections.Generic;

    #endregion


    public abstract class PayloadGenerator
    {
        private readonly Dictionary<string, string> _payload = new Dictionary<string, string>();

        public virtual IDictionary<string, string> GetPayload(IDictionary<string, string> additionalPayload = null)
        {
            if (additionalPayload == null)
            {
                return GetPayloadData();
            }

            foreach (var item in additionalPayload)
            {
                AddToPayload(item.Key, item.Value);
            }

            return GetPayloadData();
        }

        protected void AddToPayload(string key, string value)
        {
            if (_payload.ContainsKey(key))
            {
                _payload[key] = value;
            }
            else
            {
                _payload.Add(key, value);
            }
        }

        protected IDictionary<string, string> GetPayloadData() => _payload;
    }
}