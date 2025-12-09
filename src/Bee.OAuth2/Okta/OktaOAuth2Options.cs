namespace Bee.OAuth2
{
    /// <summary>
    /// Okta OAuth2 設定選項，包含 Domain、Authorization Server、Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public class OktaOAuth2Options : OAuth2Options
    {
        private string _domain = string.Empty;
        private string _authorizationServerId = "default";

        /// <summary>
        /// Okta 網域，例如: dev-123456.okta.com 或 https://dev-123456.okta.com。
        /// 設定後會自動更新相關端點。
        /// </summary>
        public string Domain
        {
            get => _domain;
            set
            {
                _domain = (value ?? string.Empty).Trim().TrimEnd('/');
                UpdateEndpoints();
            }
        }

        /// <summary>
        /// Okta 授權伺服器 ID，預設為 "default"。
        /// 設定後會自動更新相關端點。
        /// </summary>
        public string AuthorizationServerId
        {
            get => _authorizationServerId;
            set
            {
                _authorizationServerId = string.IsNullOrWhiteSpace(value) ? "default" : value.Trim().Trim('/') ;
                UpdateEndpoints();
            }
        }

        /// <summary>
        /// 建構函式。
        /// </summary>
        public OktaOAuth2Options()
        {
            Scopes = new[] { "openid", "profile", "email" };
        }

        private void UpdateEndpoints()
        {
            if (string.IsNullOrEmpty(_domain))
                return;

            var baseUrl = _domain.StartsWith("http", System.StringComparison.OrdinalIgnoreCase)
                ? _domain
                : $"https://{_domain}";

            var serverId = string.IsNullOrEmpty(_authorizationServerId) ? "default" : _authorizationServerId;

            AuthorizationEndpoint = $"{baseUrl}/oauth2/{serverId}/v1/authorize";
            TokenEndpoint = $"{baseUrl}/oauth2/{serverId}/v1/token";
            UserInfoEndpoint = $"{baseUrl}/oauth2/{serverId}/v1/userinfo";
        }
    }
}
