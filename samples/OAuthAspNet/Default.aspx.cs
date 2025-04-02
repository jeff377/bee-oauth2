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
            OAuthManager.RedirectToAuthorization("Google");
        }

        protected void btnFacebook_Click(object sender, EventArgs e)
        {
            OAuthManager.RedirectToAuthorization("Facebook");
        }

        protected void btnLine_Click(object sender, EventArgs e)
        {
            OAuthManager.RedirectToAuthorization("Line");
        }

        protected void btnAzure_Click(object sender, EventArgs e)
        {
            OAuthManager.RedirectToAuthorization("Azure");
        }
    }
}