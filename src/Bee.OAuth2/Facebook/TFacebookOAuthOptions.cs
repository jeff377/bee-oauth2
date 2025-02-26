namespace Bee.OAuth2
{
    /// <summary>
    /// Facebook OAuth2 設定選項，包含 Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class TFacebookOAuthOptions : TOAuthOptions
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public TFacebookOAuthOptions()
        {
            this.Scopes = new[] { "public_profile", "email" };
            this.AuthorizationEndpoint = "https://www.facebook.com/v18.0/dialog/oauth";
            this.TokenEndpoint = "https://graph.facebook.com/v18.0/oauth/access_token";
            this.UserInfoEndpoint = "https://graph.facebook.com/me";
        }
    }

}
