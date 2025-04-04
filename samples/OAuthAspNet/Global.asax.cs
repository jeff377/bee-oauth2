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
            if (!File.Exists(filePath)) { return; }

            var config = LoadOAuthConfig(filePath);
            RegisterIfExists("Google", config?.GoogleOAuth);
            RegisterIfExists("Facebook", config?.FacebookOAuth);
            RegisterIfExists("Line", config?.LineOAuth);
            RegisterIfExists("Azure", config?.AzureOAuth);
        }

        private OAuthConfig LoadOAuthConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Configuration file not found.", filePath);

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<OAuthConfig>(json) ?? new OAuthConfig();
        }

        private void RegisterIfExists(string name, TOAuthOptions options)
        {
            if (options != null)
            {
                OAuthManager.RegisterClient(name, new TOAuthClient(options));
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