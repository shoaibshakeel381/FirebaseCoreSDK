namespace FirebaseCoreSDK.Extensions
{
    public static class StringExtensions
    {
        public static string TrimSlashes(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }

            var trimedCharacters = new[] { '/', '\\' };
            return str.Trim(trimedCharacters);
        }
    }
}