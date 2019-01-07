namespace FirebaseCoreSDK.Firebase.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using Auth.ServiceAccounts;

    using Configuration;

    using Extensions;

    using FirebaseCoreAdmin.Firebase.Storage;

    using HttpClients.Storage;

    using Models;

    internal class FirebaseStorage : IFirebaseStorage
    {
        private readonly IServiceAccountCredentials _credentials;
        private readonly FirebaseConfiguration _configuration;
        private readonly IStorageHttpClient _httpClient;

        public FirebaseStorage(IServiceAccountCredentials credentials, FirebaseConfiguration configuration)
        {
            _credentials = credentials;
            _configuration = configuration;
            _httpClient = new StorageHttpClient(credentials, configuration);
        }

        public string GetPublicUrl(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            var normalziedPath = WebUtility.UrlEncode(path.TrimSlashes());

            var auth = _httpClient.GetAuthority().ToString().TrimSlashes();
            var fullPath = new Uri($"{auth}/{_credentials.GetDefaultBucket()}/{normalziedPath}?alt=media", UriKind.Absolute);
            return fullPath.AbsoluteUri;
        }

        public string GetSignedUrl(SigningOption options)
        {
            ValidateOptions(options);

            var signingPayloadAsString = string.Join("\n", BuildSigningPayload(options));
            var encryptedBase64String = EncryptPayload(signingPayloadAsString);

            return PrepareSignedUrl(options, encryptedBase64String);
        }

        public Task RemoveObjectAsync(string path)
        {
            var urlEncodedPath = GetUrlEncodedPath(path);
            var deleteUri = new Uri($"{_configuration.StorageBaseAuthority2}/v1/b/{_credentials.GetDefaultBucket()}/o/{urlEncodedPath}", UriKind.Absolute);
            return _httpClient.SendStorageRequestAsync<object>(deleteUri, HttpMethod.Delete);
        }

        public async Task<(bool Result, Exception ex)> TryRemoveObjectAsync(string path)
        {
            try
            {
                await RemoveObjectAsync(path).ConfigureAwait(false);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public Task<ObjectMetadata> GetObjectMetaDataAsync(string path)
        {
            var urlEncodedPath = GetUrlEncodedPath(path);
            var metaUri = new Uri($"{ _configuration.StorageBaseAuthority2}/v1/b/{_credentials.GetDefaultBucket()}/o/{urlEncodedPath}", UriKind.Absolute);
            return _httpClient.SendStorageRequestAsync<ObjectMetadata>(metaUri, HttpMethod.Get);
        }

        public async Task MoveObjectAsync(string originPath, string destinationPath)
        {
            var urlEncodedOrigin = GetUrlEncodedPath(originPath);
            var urlEncodedDestination = GetUrlEncodedPath(destinationPath);

            var reWriteUri = new Uri($"{ _configuration.StorageBaseAuthority2}/v1/b/{_credentials.GetDefaultBucket()}/o/{urlEncodedOrigin}/rewriteTo/b/{_credentials.GetDefaultBucket()}/o/{urlEncodedDestination}", UriKind.Absolute);
            await _httpClient.SendStorageRequestAsync<object>(reWriteUri, HttpMethod.Post).ConfigureAwait(false);

            await RemoveObjectAsync(originPath).ConfigureAwait(false);
        }

        public async Task<(bool Result, Exception ex)> TryMoveObjectAsync(string originPat, string destinationPath)
        {
            try
            {
                await MoveObjectAsync(originPat, destinationPath).ConfigureAwait(false);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        #region Private Methods
        private string GetUrlEncodedPath(string path)
        {
            string normalizedPath = path.TrimSlashes();

            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            return WebUtility.UrlEncode(normalizedPath);
        }

        private void ValidateOptions(SigningOption options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (options.ExpireDate.ToUnixSeconds() == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(options.ExpireDate), "ExpireDate should be reasonable value");
            }
            if (String.IsNullOrWhiteSpace(options.Path))
            {
                throw new ArgumentNullException(nameof(options.Path));
            }
        }

        private string PrepareSignedUrl(SigningOption options, string signature)
        {
            var uri = new UriBuilder
            {
                Scheme = "https",
                Host =
                    $"{_configuration.StorageBaseAuthority.TrimSlashes().Replace("https://", "")}/{_credentials.GetDefaultBucket().TrimSlashes()}",
                Path = options.Path.Trim().TrimSlashes(),
                Query =
                    $"GoogleAccessId={_credentials.GetServiceAccountEmail()}&Expires={BuildExpiration(options.ExpireDate)}&Signature={WebUtility.UrlEncode(signature)}"
            };

            return uri.Uri.AbsoluteUri;
        }

        private string EncryptPayload(string signingPayloadAsString)
        {
            string encryptedBase64String;
            UTF8Encoding byteConverter = new UTF8Encoding();
            var payloadBytes = byteConverter.GetBytes(signingPayloadAsString);

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_credentials.GetRSAParams());
                var encrypt = rsa.SignData(payloadBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                encryptedBase64String = Convert.ToBase64String(encrypt);
            }
            return encryptedBase64String;
        }

        private IList<string> BuildSigningPayload(SigningOption options)
        {
            var payload = new List<string>
            {
                BuildActionMethod(options.Action),
                BuildContentMD5(options.ContentMD5),
                BuildContentType(options.ContentType),
                BuildExpiration(options.ExpireDate),
                BuilCanonicalizedResource(options.Path)
            };

            return payload;
        }

        private string BuildActionMethod(SigningAction action)
        {
            string actionMethod = string.Empty;
            switch (action)
            {
                case SigningAction.Read:
                    actionMethod = "GET";
                    break;
                case SigningAction.Write:
                    actionMethod = "PUT";
                    break;
                case SigningAction.Delete:
                    actionMethod = "DELETE";
                    break;
            }

            return actionMethod;
        }

        private string BuildContentMD5(string contentMd5)
        {
            return string.IsNullOrWhiteSpace(contentMd5) ? string.Empty : contentMd5.Trim();
        }

        private string BuildContentType(string contentType)
        {
            return string.IsNullOrWhiteSpace(contentType) ? string.Empty : contentType.Trim();
        }

        private string BuildExpiration(DateTime dateTo)
        {
            return dateTo.ToUnixSeconds().ToString();
        }

        private string BuilCanonicalizedResource(string path)
        {
            return $"/{_credentials.GetDefaultBucket().Trim().TrimSlashes()}/{WebUtility.UrlEncode(path.TrimSlashes())}";
        }
        #endregion

        #region Dispose Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _httpClient?.Dispose();

        }

        ~FirebaseStorage()
        {
            Dispose(false);
        }
        #endregion
    }
}
