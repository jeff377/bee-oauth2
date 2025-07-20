using Bee.OAuth2;

namespace OAuthAspNetCore.Models
{
    public class OAuthConfig
    {
        public GoogleOAuth2Options? GoogleOAuth { get; set; }
        public FacebookOAuth2Options? FacebookOAuth { get; set; }
        public LineOAuth2Options? LineOAuth { get; set; }
        public AzureOAuth2Options? AzureOAuth { get; set; }
    }
}
