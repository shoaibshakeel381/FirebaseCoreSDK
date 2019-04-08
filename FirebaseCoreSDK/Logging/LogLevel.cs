namespace FirebaseCoreSDK.Logging
{
    #region Namespace Imports

    using System;

    #endregion


    [Flags]
    public enum LogLevel
    {
        None = 0,
        Debug = 1 << 1,
        Info = 1 << 2,
        Warning = 1 << 3,
        Error = 1 << 4
    }
}