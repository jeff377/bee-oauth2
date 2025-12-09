using System;
using System.IO;
using Bee.OAuth2;
using Bee.OAuth2.AspNet;
using Newtonsoft.Json;

namespace OAuthAspNet
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			string filePath = Server.MapPath("~/App_Data/OAuthConfig.json");
			// string filePath = @"D:\Config\WebOAuthConfig.json";
            if (!File.Exists(filePath)) { return; }

            var config = LoadOAuthConfig(filePath);
            RegisterIfExists("Google", config?.GoogleOAuth);
            RegisterIfExists("Facebook", config?.FacebookOAuth);
            RegisterIfExists("Line", config?.LineOAuth);
            RegisterIfExists("Azure", config?.AzureOAuth);
                        RegisterIfExists("Auth0", config?.Auth0OAuth);
            RegisterIfExists("Okta", config?.OktaOAuth);
        }

        private OAuthConfig LoadOAuthConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Configuration file not found.", filePath);

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<OAuthConfig>(json) ?? new OAuthConfig();
        }

        private void RegisterIfExists(string name, OAuth2Options options)
        {
            if (options != null)
            {
                OAuth2Manager.RegisterClient(name, new OAuth2Client(options));
            }
        }

        protected void Session_Start(object sender, EventArgs e) 
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e) 
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e) 
		{

		}

		protected void Application_Error(object sender, EventArgs e) 
		{

		}

		protected void Session_End(object sender, EventArgs e) 
		{

		}

		protected void Application_End(object sender, EventArgs e) 
		{

		}
	}
}