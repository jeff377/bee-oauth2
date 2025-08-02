namespace Bee.OAuth2
{
    /// <summary>
    /// Auth0 OAuth2 設定選項，包含 Domain、Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class Auth0OAuth2Options : OAuth2Options
    {
        private string _domain = string.Empty;

        /// <summary>
        /// Auth0 Domain，例如: your-tenant.auth0.com。
        /// 設定後會自動更新相關端點。
        /// </summary>
        public string Domain
        {
            get => _domain;
            set
            {
                _domain = (value ?? string.Empty).TrimEnd('/');
                if (string.IsNullOrEmpty(_domain))
                    return;

                AuthorizationEndpoint = $"https://{_domain}/authorize";
                TokenEndpoint = $"https://{_domain}/oauth/token";
                UserInfoEndpoint = $"https://{_domain}/userinfo";
            }
        }

        /// <summary>
        /// 建構函式。
        /// </summary>
        public Auth0OAuth2Options()
        {
            Scopes = new[] { "openid", "profile", "email" };
        }
    }
}
