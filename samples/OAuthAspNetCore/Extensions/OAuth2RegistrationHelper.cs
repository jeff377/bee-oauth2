using Bee.OAuth2;
using Bee.OAuth2.AspNetCore;
using Newtonsoft.Json;
using OAuthAspNetCore.Models;

namespace OAuthAspNetCore.Extensions
{
    public static class OAuth2RegistrationHelper
    {
        public static OAuth2Manager CreateOAuth2Manager(IServiceProvider provider)
        {
            var accessor = provider.GetRequiredService<IHttpContextAccessor>();
            var config = LoadOAuthConfig(@"OAuthConfig.json");
            var manager = new OAuth2Manager(accessor);
            RegisterIfExists(manager, "Google", config?.GoogleOAuth, accessor);
            RegisterIfExists(manager, "Facebook", config?.FacebookOAuth, accessor);
            RegisterIfExists(manager, "Line", config?.LineOAuth, accessor);
            RegisterIfExists(manager, "Azure", config?.AzureOAuth, accessor);
            return manager;
        }

        private static OAuthConfig LoadOAuthConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Configuration file not found.", filePath);

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<OAuthConfig>(json) ?? new OAuthConfig();
        }

        private static void RegisterIfExists(OAuth2Manager manager, string name, OAuth2Options? options, IHttpContextAccessor accessor)
        {
            if (options != null)
            {
                manager.RegisterClient(name, new OAuth2Client(options, accessor));
            }
        }
    }

}
