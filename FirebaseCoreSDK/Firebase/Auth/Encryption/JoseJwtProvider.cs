namespace FirebaseCoreSDK.Firebase.Auth.Encryption
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    using Jose;

    public class JoseJwtProvider : IJwtProvider
    {
        public string Encode(IDictionary<string, string> payload, RSA privateKey)
        {
            if (payload == null || payload.Count == 0)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (privateKey == null)
            {
                throw new ArgumentNullException(nameof(privateKey));
            }
            return JWT.Encode(payload, privateKey, JwsAlgorithm.RS256);
        }
    }
}
