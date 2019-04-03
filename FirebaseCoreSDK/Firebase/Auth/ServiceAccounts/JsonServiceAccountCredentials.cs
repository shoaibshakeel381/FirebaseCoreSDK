namespace FirebaseCoreSDK.Firebase.Auth.ServiceAccounts
{
    #region Namespace Imports

    using System.IO;
    using System.Security.Cryptography;

    using FirebaseCoreSDK.Extensions;
    using FirebaseCoreSDK.Firebase.Auth.Models;

    using Newtonsoft.Json;

    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.OpenSsl;

    #endregion


    public class JsonServiceAccountCredentials : IServiceAccountCredentials
    {
        private JsonServiceAccountModel _credentialsData;

        private RSAParameters _rsaParam;

        public JsonServiceAccountCredentials(string fileName) => InitializeFromFile(fileName);

        /// <summary>
        /// </summary>
        /// <param name="content">
        ///     JSON string containing service account credentials exported from firebase.<br />
        ///     JSON should contain values for project_id, private_key and client_email
        /// </param>
        /// <param name="fromFile"></param>
        public JsonServiceAccountCredentials(string content, bool fromFile)
        {
            if (fromFile)
            {
                InitializeFromFile(content);
            }
            else
            {
                InitializeFromString(content);
            }
        }

        public virtual string GetDefaultBucket() => $"{_credentialsData.ProjectId}.appspot.com";

        public virtual string GetProjectId() => _credentialsData.ProjectId;

        public RSAParameters GetRSAParams() => _rsaParam;

        public virtual string GetServiceAccountEmail() => _credentialsData.ClientEmail;

        private void FillRsaParams()
        {
            RsaPrivateCrtKeyParameters key;

            using (var sr = new StringReader(_credentialsData.PrivateKey))
            {
                var pr = new PemReader(sr);
                key = (RsaPrivateCrtKeyParameters)pr.ReadObject();
            }

            _rsaParam = key.ToRSAParameters();
        }

        private void InitializeFromFile(string fileName)
        {
            _credentialsData = JsonConvert.DeserializeObject<JsonServiceAccountModel>(File.ReadAllText(fileName));

            if (_credentialsData == null)
            {
                throw new FileLoadException("Incorrect json file was provided");
            }

            FillRsaParams();
        }

        private void InitializeFromString(string content)
        {
            _credentialsData = JsonConvert.DeserializeObject<JsonServiceAccountModel>(content);

            if (_credentialsData == null)
            {
                throw new FileLoadException("Incorrect json file was provided");
            }

            FillRsaParams();
        }
    }
}