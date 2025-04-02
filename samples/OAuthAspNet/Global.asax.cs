using System;
using System.IO;
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
            var googleOptions = config?.GoogleOAuth;
			if (googleOptions != null)
            {
                OAuthManager.RegisterClient("Google", new TOAuthClient(googleOptions));
            }
			var facebookOptions = config?.FacebookOAuth;
			if (facebookOptions != null)
            {
                OAuthManager.RegisterClient("Facebook", new TOAuthClient(facebookOptions));
            }
            var lineOptions = config?.LineOAuth;
			if (lineOptions != null)
            {
                OAuthManager.RegisterClient("Line", new TOAuthClient(lineOptions));
            }
            var azureOptions = config?.AzureOAuth;
            if (azureOptions != null)
            {
                OAuthManager.RegisterClient("Azure", new TOAuthClient(azureOptions));
            }	
        }

        private OAuthConfig LoadOAuthConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Configuration file not found.", filePath);

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<OAuthConfig>(json) ?? new OAuthConfig();
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