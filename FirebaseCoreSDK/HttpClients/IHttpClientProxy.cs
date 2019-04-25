namespace FirebaseCoreSDK.HttpClients
{
    #region Namespace Imports

    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    #endregion


    /// <summary>
    ///     This will help clients mock or override http calls
    /// </summary>
    public interface IHttpClientProxy
    {
        /// <summary>Send an HTTP request as an asynchronous operation.</summary>
        /// <param name="request">The HTTP request message to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<string> SendAsync(Func<HttpRequestMessage> request);
    }
}