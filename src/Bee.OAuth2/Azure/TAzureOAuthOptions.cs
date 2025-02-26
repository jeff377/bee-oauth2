namespace Bee.OAuth2
{
    /// <summary>
    /// Azure OAuth2 設定選項，包含 Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class TAzureOAuthOptions : TOAuthOptions
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public TAzureOAuthOptions()
        {
            this.Scopes = new[] { "openid", "profile", "email" };
            this.AuthorizationEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
            this.TokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
            this.UserInfoEndpoint = "https://graph.microsoft.com/oidc/userinfo";
        }
    }

}
