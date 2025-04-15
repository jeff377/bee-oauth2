namespace Bee.OAuth2
{
    /// <summary>
    /// OAuth2 設定選項基底類別，包含 Client ID、Secret、Redirect URI 及相關端點。
    /// </summary>
    public abstract class TOAuth2Options
    {
        /// <summary>
        /// OAuth2 應用程式的 Client ID（用於識別應用）。
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// OAuth2 應用程式的 Client Secret（用於驗證應用）。
        /// 請妥善保管此值，避免洩漏。
        /// </summary>
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>
        /// OAuth2 回調網址，OAuth2 驗證流程完成後，會將使用者重定向到此 URI。
        /// 必須與 Google Cloud Console 中設定的 Redirect URI 相符。
        /// </summary>
        public string RedirectUri { get; set; } = string.Empty;

        /// <summary>
        /// 申請的 OAuth2 權限範圍（Scopes）。
        /// </summary>
        public string[] Scopes { get; set; } = new[] { "openid", "email", "profile" };

        /// <summary>
        /// OAuth2 授權端點 (Authorization Endpoint)。
        /// </summary>
        public string AuthorizationEndpoint { get; set; } = string.Empty;

        /// <summary>
        /// OAuth2 令牌端點 (Token Endpoint)。
        /// 用於交換授權碼 (Authorization Code) 以取得 Access Token。
        /// </summary>
        public string TokenEndpoint { get; set; } = string.Empty;

        /// <summary>
        /// OAuth2 用戶資訊端點 (UserInfo Endpoint)。
        /// 用於取得用戶身份資訊，如名稱、Email、頭像等。
        /// </summary>
        public string UserInfoEndpoint { get; set; } = string.Empty;

        /// <summary>
        /// 是否使用 PKCE 驗證。
        /// </summary>
        public bool UsePkce { get; set; } = false;
    }
}
