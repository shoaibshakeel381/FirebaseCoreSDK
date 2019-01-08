namespace FirebaseCoreSDK.Firebase.Database
{
    using System;

    public interface IFirebaseDatabase : IDisposable
    {
        IDatabaseRef Ref(string path);
    }
}