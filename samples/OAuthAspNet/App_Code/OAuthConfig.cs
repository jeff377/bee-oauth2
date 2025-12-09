using Bee.OAuth2;

namespace OAuthAspNet
{
    public class OAuthConfig
    {
        public GoogleOAuth2Options GoogleOAuth { get; set; }
        public FacebookOAuth2Options FacebookOAuth { get; set; }
        public LineOAuth2Options LineOAuth { get; set; }
        public AzureOAuth2Options AzureOAuth { get; set; }
        public Auth0OAuth2Options Auth0OAuth { get; set; }
        public OktaOAuth2Options OktaOAuth { get; set; }
    }
}
