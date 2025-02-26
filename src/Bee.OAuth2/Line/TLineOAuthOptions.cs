namespace Bee.OAuth2
{
    /// <summary>
    /// LINE OAuth2 設定選項，包含 Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class TLineOAuthOptions : TOAuthOptions
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public TLineOAuthOptions()
        {
            this.Scopes = new[] { "profile", "openid", "email" };
            this.AuthorizationEndpoint = "https://access.line.me/oauth2/v2.1/authorize";
            this.TokenEndpoint = "https://api.line.me/oauth2/v2.1/token";
            this.UserInfoEndpoint = "https://api.line.me/v2/profile";
        }
    }

}
