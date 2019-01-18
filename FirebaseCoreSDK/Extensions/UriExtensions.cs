namespace FirebaseCoreSDK.Extensions
{
    using System;
    using UrlCombineLib;

    public static class UriExtensions
    {
        public static Uri Append(this Uri baseUri, Uri relativeUri)
        {
            return baseUri.Append(relativeUri.ToString());
        }

        public static Uri Append(this Uri baseUri, string relativeUri)
        {
            return baseUri.Combine(relativeUri); ;
        }
    }
}
