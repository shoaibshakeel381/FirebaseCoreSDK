namespace FirebaseCoreSDK.HttpClients
{
    using System;

    public interface IHttpClient : IDisposable
    {
        Uri GetAuthority();
    }
}
