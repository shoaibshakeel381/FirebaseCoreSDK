namespace FirebaseCoreSDK.Exceptions
{
    #region Namespace Imports

    using System;

    #endregion


    public class FirebaseException : Exception
    {
        public FirebaseException() {}

        public FirebaseException(string message)
            : base(message) {}

        public FirebaseException(string message, Exception innerException)
            : base(message, innerException) {}

        public FirebaseException(Exception innerException)
            : base(innerException.Message, innerException) {}
    }
}