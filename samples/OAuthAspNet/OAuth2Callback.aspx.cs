﻿using System;
using Bee.OAuth2.AspNet;

namespace OAuthAspNet
{
	public partial class OAuth2Callback : System.Web.UI.Page
	{
		protected async void Page_Load(object sender, EventArgs e)
		{
            var result = await OAuthManager.ValidateAuthorization();
            Response.Write(
                $"ProviderName : {result.ProviderName}<br/>" +
                $"UserID : {result.UserInfo.UserId}<br/>" +
                $"UserName : {result.UserInfo.UserName}<br/>" +
                $"Email : {result.UserInfo.Email}<br/>" +
                $"RawJson : {result.UserInfo.RawJson}");
        }
	}
}