using System;
using Bee.OAuth2.AspNet;

namespace OAuthAspNet
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnGoogle_Click(object sender, EventArgs e)
        {
            OAuth2Manager.RedirectToAuthorization("Google");
        }

        protected void btnFacebook_Click(object sender, EventArgs e)
        {
            OAuth2Manager.RedirectToAuthorization("Facebook");
        }

        protected void btnLine_Click(object sender, EventArgs e)
        {
            OAuth2Manager.RedirectToAuthorization("Line");
        }

        protected void btnAzure_Click(object sender, EventArgs e)
        {
            OAuth2Manager.RedirectToAuthorization("Azure");
        }

        protected void btnAuth0_Click(object sender, EventArgs e)
        {
            OAuth2Manager.RedirectToAuthorization("Auth0");
        }
    }
}