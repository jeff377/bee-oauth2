namespace Bee.OAuth2
{
    /// <summary>
    /// Google OAuth2 設定選項，包含 Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class TGoogleOAuth2Options : TOAuth2Options
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public TGoogleOAuth2Options()
        {
            Scopes = new[] { "openid", "email", "profile" };
            AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
            TokenEndpoint = "https://oauth2.googleapis.com/token";
            UserInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";
        }
    }

}
