using Bee.OAuth2.AspNetCore;
using OAuthAspNetCore.Models;

namespace OAuthAspNetCore.Extensions
{
    public static class OAuth2RegistrationHelper
    {
        public static TOAuth2Manager CreateOAuth2Manager(IServiceProvider provider)
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var accessor = provider.GetRequiredService<IHttpContextAccessor>();

            var oauthConfig = config.Get<OAuthConfig>();
            var manager = new TOAuth2Manager(accessor);

            if (oauthConfig == null)
            {
                throw new InvalidOperationException("OAuth configuration is missing.");
            }

            if (oauthConfig.GoogleOAuth != null)
            {
                manager.RegisterClient("Google", new TOAuth2Client(oauthConfig.GoogleOAuth, accessor));
            }
            if (oauthConfig.FacebookOAuth != null)
            {
                manager.RegisterClient("Facebook", new TOAuth2Client(oauthConfig.FacebookOAuth, accessor));
            }
            if (oauthConfig.LineOAuth != null)
            {
                manager.RegisterClient("Line", new TOAuth2Client(oauthConfig.LineOAuth, accessor));
            }
            if (oauthConfig.AzureOAuth != null)
            {
                manager.RegisterClient("Azure", new TOAuth2Client(oauthConfig.AzureOAuth, accessor));
            }

            return manager;
        }
    }

}
