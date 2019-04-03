namespace FirebaseCoreSDK.Firebase.Database
{
    #region Namespace Imports

    using System;

    #endregion


    public interface IFirebaseDatabase : IDisposable
    {
        IDatabaseRef Ref(string path);
    }
}