namespace FirebaseCoreSDK.Extensions
{
    #region Namespace Imports

    using System.Security.Cryptography;

    using Org.BouncyCastle.Crypto.Parameters;

    #endregion


    public static class EncryptionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static RSAParameters ToRSAParameters(this RsaPrivateCrtKeyParameters privKey)
        {
            var rp = new RSAParameters
            {
                Modulus = privKey.Modulus.ToByteArrayUnsigned(),
                Exponent = privKey.PublicExponent.ToByteArrayUnsigned(),
                D = privKey.Exponent.ToByteArrayUnsigned(),
                P = privKey.P.ToByteArrayUnsigned(),
                Q = privKey.Q.ToByteArrayUnsigned(),
                DP = privKey.DP.ToByteArrayUnsigned(),
                DQ = privKey.DQ.ToByteArrayUnsigned(),
                InverseQ = privKey.QInv.ToByteArrayUnsigned()
            };

            return rp;
        }
    }
}