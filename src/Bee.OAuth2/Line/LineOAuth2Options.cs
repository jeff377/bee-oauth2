namespace Bee.OAuth2
{
    /// <summary>
    /// LINE OAuth2 設定選項，包含 Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class LineOAuth2Options : OAuth2Options
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public LineOAuth2Options()
        {
            Scopes = new[] { "profile", "openid", "email" };
            AuthorizationEndpoint = "https://access.line.me/oauth2/v2.1/authorize";
            TokenEndpoint = "https://api.line.me/oauth2/v2.1/token";
            UserInfoEndpoint = "https://api.line.me/v2/profile";
        }
    }

}
