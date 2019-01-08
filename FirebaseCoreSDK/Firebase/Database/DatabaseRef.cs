namespace FirebaseCoreSDK.Firebase.Database
{
    using System;
    using System.Collections.Generic;

    using Configuration;

    using Extensions;

    using HttpClients.Database;

    public class DatabaseRef : IDatabaseRef
    {
        internal readonly IDatabaseHttpClient HttpClient;
        private readonly FirebaseConfiguration _configuration;
        private readonly List<KeyValuePair<string, string>> _queryStore = new List<KeyValuePair<string, string>>();

        public string Path { get; set; }

        public DatabaseRef(IDatabaseHttpClient httpClient, FirebaseConfiguration configuration, string refPath)
        {
            if (string.IsNullOrWhiteSpace(refPath))
            {
                throw new ArgumentNullException(nameof(refPath));
            }

            Path = $"{refPath.TrimSlashes()}.json";

            HttpClient = httpClient;
            _configuration = configuration;
        }

        /// <exception cref="ArgumentNullException">
        /// <paramref name="key"/> is <see langword="null"/><br/>
        /// <paramref name="value"/> is <see langword="null"/>
        /// </exception>
        public void Add(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            _queryStore.Add(new KeyValuePair<string, string>(key, value));
        }

        /// <exception cref="ArgumentNullException">
        /// key is <see langword="null"/><br/>
        /// value is <see langword="null"/>
        /// </exception>
        public void AddBool(string key, bool value)
        {
            var valueString = value ? "true" : "false";
            Add(key, valueString);
        }

        /// <exception cref="ArgumentNullException">
        /// key is <see langword="null"/><br/>
        /// value is <see langword="null"/>
        /// </exception>
        public void AddString(string key, string value)
        {
            Add(key, $"\"{value}\"");
        }

        public IList<KeyValuePair<string, string>> GetQueryStore()
        {
            return _queryStore;
        }
    }
}
