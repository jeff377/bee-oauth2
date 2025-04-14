using Bee.OAuth2;

namespace OAuthAspNetCore.Models
{
    public class OAuthConfig
    {
        public TGoogleOAuth2Options? GoogleOAuth { get; set; }
        public TFacebookOAuth2Options? FacebookOAuth { get; set; }
        public TLineOAuth2Options? LineOAuth { get; set; }
        public TAzureOAuth2Options? AzureOAuth { get; set; }
    }
}
