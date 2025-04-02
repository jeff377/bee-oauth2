using Bee.OAuth2;

namespace OAuthWinForms
{
    public class OAuthConfig
    {
        public TGoogleOAuthOptions GoogleOAuth { get; set; }
        public TFacebookOAuthOptions FacebookOAuth { get; set; }
        public TLineOAuthOptions LineOAuth { get; set; }
        public TAzureOAuthOptions AzureOAuth { get; set; }
    }
}
