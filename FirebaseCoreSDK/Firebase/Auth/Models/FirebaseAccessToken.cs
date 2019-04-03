namespace FirebaseCoreSDK.Firebase.Auth.Models
{
    public class FirebaseAccessToken
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }
    }
}