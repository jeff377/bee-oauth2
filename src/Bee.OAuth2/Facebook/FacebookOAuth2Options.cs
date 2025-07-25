﻿namespace Bee.OAuth2
{
    /// <summary>
    /// Facebook OAuth2 設定選項，包含 Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class FacebookOAuth2Options : OAuth2Options
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public FacebookOAuth2Options()
        {
            Scopes = new[] { "public_profile", "email" };
            AuthorizationEndpoint = "https://www.facebook.com/v18.0/dialog/oauth";
            TokenEndpoint = "https://graph.facebook.com/v18.0/oauth/access_token";
            UserInfoEndpoint = "https://graph.facebook.com/me";
        }
    }

}
