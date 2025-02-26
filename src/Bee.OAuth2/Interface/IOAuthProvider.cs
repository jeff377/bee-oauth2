using System.Threading.Tasks;

namespace Bee.OAuth2
{
    /// <summary>
    /// OAuth2 驗證服務提供者介面。
    /// </summary>
    public interface IOAuthProvider
    {
        /// <summary>
        /// OAuth2 驗證服務提供者名稱。
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// 產生 OAuth2 授權 URL，讓使用者登入並授權應用程式。
        /// </summary>
        /// <param name="state">用於防止 CSRF 的隨機字串</param>
        /// <returns>OAuth2 授權 URL</returns>
        string GetAuthorizationUrl(string state);

        /// <summary>
        /// 產生 OAuth2 授權 URL，讓使用者登入並授權應用程式。
        /// </summary>
        /// <param name="state">用於防止 CSRF 的隨機字串</param>
        /// <param name="codeChallenge">code_verifier 的 SHA-256 雜湊值。</param>
        /// <returns>OAuth2 授權 URL</returns>
        string GetAuthorizationUrl(string state, string codeChallenge);

        /// <summary>
        /// 取得 OAuth2 驗證流程完成後的回呼網址。
        /// </summary>
        string GetRedirectUri();

        /// <summary>
        /// 透過授權碼 (Authorization Code) 交換 Access Token。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼 (Authorization Code)。</param>
        /// <returns>Access Token</returns>
        Task<string> GetAccessTokenAsync(string authorizationCode);

        /// <summary>
        /// 透過授權碼 (Authorization Code) 交換 Access Token。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼 (Authorization Code)。</param>
        /// <param name="codeVerifier">用戶端產生的隨機字串，用來驗證授權碼請求的合法性。</param>
        /// <returns>Access Token</returns>
        Task<string> GetAccessTokenAsync(string authorizationCode, string codeVerifier);

        /// <summary>
        /// 透過 Access Token 取得用戶資訊。
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <returns>用戶資訊 JSON 字串</returns>
        Task<string> GetUserInfoAsync(string accessToken);

        /// <summary>
        /// 解析用戶資訊 JSON 字串。
        /// </summary>
        /// <param name="json">用戶資訊 JSON 字串。</param>
        TUserInfo ParseUserJson(string json);

        /// <summary>
        /// 使用 Refresh Token 取得新的 Access Token。
        /// </summary>
        /// <param name="refreshToken">Refresh Token</param>
        /// <returns>新的 Access Token</returns>
        Task<string> RefreshAccessTokenAsync(string refreshToken);
    }
}
