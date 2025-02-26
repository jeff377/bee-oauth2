namespace Bee.OAuth2
{
    /// <summary>
    /// Google OAuth2 設定選項，包含 Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class TGoogleOAuthOptions : TOAuthOptions
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public TGoogleOAuthOptions()
        {
            this.Scopes = new[] { "openid", "email", "profile" };
            this.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
            this.TokenEndpoint = "https://oauth2.googleapis.com/token";
            this.UserInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";
        }
    }

}
