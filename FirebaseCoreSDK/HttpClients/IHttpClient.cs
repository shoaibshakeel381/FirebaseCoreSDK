namespace FirebaseCoreSDK.HttpClients
{
    using System;

    internal interface IHttpClient : IDisposable
    {
        Uri GetAuthority();
    }
}
