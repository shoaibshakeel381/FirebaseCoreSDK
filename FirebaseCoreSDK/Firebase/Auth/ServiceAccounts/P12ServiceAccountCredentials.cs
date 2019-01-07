namespace FirebaseCoreSDK.Firebase.Auth.ServiceAccounts
{
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    public class P12ServiceAccountCredentials : IServiceAccountCredentials
    {
        private readonly string _fileName;
        private readonly string _password;
        private readonly string _serviceAccountEmail;
        private readonly string _projectId;

        public P12ServiceAccountCredentials(string fileName, string password, string serviceAccountEmail, string projectId)
        {
            _serviceAccountEmail = serviceAccountEmail;
            _fileName = fileName;
            _password = password;
            _projectId = projectId;
        }

        public string GetDefaultBucket()
        {
            return $"{_projectId}.appspot.com";
        }

        public string GetProjectId()
        {
            return _projectId;
        }

        /// <exception cref="CryptographicException">An error with the certificate occurs. For example:  
        /// The certificate file does not exist.  
        /// The certificate is invalid.  
        /// The certificate&amp;#39;s password is incorrect.</exception>
        public RSAParameters GetRSAParams()
        {
            using (var cert = new X509Certificate2(_fileName, _password))
            {
                return cert.GetRSAPrivateKey().ExportParameters(true);
            }
        }

        public string GetServiceAccountEmail()
        {
            return _serviceAccountEmail;
        }
    }
}
