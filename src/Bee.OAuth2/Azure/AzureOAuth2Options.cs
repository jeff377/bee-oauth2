﻿namespace Bee.OAuth2
{
    /// <summary>
    /// Azure OAuth2 設定選項，包含 Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class AzureOAuth2Options : OAuth2Options
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public AzureOAuth2Options()
        {
            Scopes = new[] { "openid", "profile", "email" };
            AuthorizationEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
            TokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
            UserInfoEndpoint = "https://graph.microsoft.com/oidc/userinfo";
        }
    }

}
