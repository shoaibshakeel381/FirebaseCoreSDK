namespace FirebaseCoreSDK.Firebase.Database
{
    #region Namespace Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion


    public class QueryBuilder
    {
        private readonly string _initialQuery;
        private readonly string endAtParam = "endAt";
        private readonly string eqaulToParam = "equalTo";
        private readonly string formatParam = "format";
        private readonly string formatVal = "export";
        private readonly string limitToFirstParam = "limitToFirst";
        private readonly string limitToLastParam = "limitToLast";
        private readonly string orderByParam = "orderBy";
        private readonly string printParam = "print";
        private readonly string shallowParam = "shallow";
        private readonly string startAtParam = "startAt";

        private static Dictionary<string, object> _query = new Dictionary<string, object>();

        private QueryBuilder(string initialQuery = "")
        {
            _initialQuery = initialQuery;
            _query = new Dictionary<string, object>();
        }

        public static QueryBuilder New(string initialQuery = "") => new QueryBuilder(initialQuery);

        public QueryBuilder EndAt(string value) => AddToQueryDictionary(endAtParam, value);

        public QueryBuilder EndAt(long value) => AddToQueryDictionary(endAtParam, value);

        public QueryBuilder EqualTo(string value) => AddToQueryDictionary(eqaulToParam, value);

        public QueryBuilder IncludePriority(bool value) => AddToQueryDictionary(formatParam, value ? formatVal : string.Empty, true);

        public QueryBuilder LimitToFirst(int value) => AddToQueryDictionary(limitToFirstParam, value > 0 ? value.ToString() : string.Empty, true);

        public QueryBuilder LimitToLast(int value) => AddToQueryDictionary(limitToLastParam, value > 0 ? value.ToString() : string.Empty, true);

        public QueryBuilder OrderBy(string value) => AddToQueryDictionary(orderByParam, value);

        public QueryBuilder Print(string value) => AddToQueryDictionary(printParam, value, true);


        public QueryBuilder Shallow(bool value) => AddToQueryDictionary(shallowParam, value ? "true" : string.Empty, true);

        public QueryBuilder StartAt(string value) => AddToQueryDictionary(startAtParam, value);

        public QueryBuilder StartAt(long value) => AddToQueryDictionary(startAtParam, value);

        public string ToQueryString()
        {
            if (!_query.Any() && !string.IsNullOrEmpty(_initialQuery))
            {
                return _initialQuery;
            }

            return !string.IsNullOrEmpty(_initialQuery)
                ? $"{_initialQuery}&{string.Join("&", _query.Select(pair => $"{pair.Key}={pair.Value}").ToArray())}"
                : string.Join("&", _query.Select(pair => $"{pair.Key}={pair.Value}").ToArray());
        }

        private QueryBuilder AddToQueryDictionary(string parameterName, string value, bool skipEncoding = false)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _query.Add(parameterName, skipEncoding ? value : EscapeString(value));
            }
            else
            {
                _query.Remove(startAtParam);
            }

            return this;
        }

        private QueryBuilder AddToQueryDictionary(string parameterName, long value)
        {
            _query.Add(parameterName, value);
            return this;
        }

        private string EscapeString(string value) => $"\"{Uri.EscapeDataString(value).Replace("%20", "+").Trim('\"')}\"";
    }
}