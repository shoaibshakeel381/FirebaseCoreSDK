namespace FirebaseCoreSDK.Extensions
{
    #region Namespace Imports

    using System;

    using UrlCombineLib;

    #endregion


    public static class UriExtensions
    {
        public static Uri Append(this Uri baseUri, Uri relativeUri) => baseUri.Append(relativeUri.ToString());

        public static Uri Append(this Uri baseUri, string relativeUri) => baseUri.Combine(relativeUri);
    }
}