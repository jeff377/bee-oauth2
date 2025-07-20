using System.Text;
using Microsoft.AspNetCore.Http;

namespace Bee.OAuth2.AspNetCore
{
    /// <summary>
    /// 提供 ASP.NET Core 程式的 OAuth2 驗證流程中的狀態儲存機制。
    /// </summary>
    public class StateStorage : IStateStorage
    {
        private const string StateKey = "_StateKey";
        private const string CodeVerifierKey = "_CodeVerifierKey";
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="httpContextAccessor">提供目前 HttpContext 的存取權。</param>
        public StateStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 儲存 `state` 參數值。
        /// </summary>
        /// <param name="value">儲存的狀態值，例如隨機產生的 `state` 字串。</param>
        public void SaveState(string value)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return;

            context.Response.Cookies.Append(StateKey, value, new CookieOptions
            {
                HttpOnly = true,  // 防止 JavaScript 存取，避免 XSS 攻擊
                Secure = true,  // 只允許 HTTPS 傳輸，避免中間人攻擊
                SameSite = SameSiteMode.None,  // 允許跨站傳遞（避免跨網站登入問題）
                Expires = DateTimeOffset.UtcNow.AddMinutes(10)  // 設定有效時間為 10 分鐘
            });
        }

        /// <summary>
        /// 取得 `state` 參數值，用於驗證 OAuth2 callback 時返回的 `state` 是否一致。
        /// </summary>
        /// <returns>返回儲存的 `state` 值，如果不存在則回傳 `null`。</returns>
        public string? GetState()
        {
            var context = _httpContextAccessor.HttpContext;
            return context?.Request.Cookies[StateKey];
        }

        /// <summary>
        /// 移除 `state` 參數值，通常在 OAuth2 驗證完成後清除已使用的 `state` 值。
        /// </summary>
        public void RemoveState()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return;

            context.Response.Cookies.Delete(StateKey);
        }

        /// <summary>
        /// 使用 PKCE 驗證時，儲存 `code_Verifier` 參數值。
        /// </summary>
        /// <param name="codeVerifier">用戶端隨機產生的 `code_Verifier`  字串。</param>
        public void SaveCodeVerifier(string codeVerifier)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return;

            // Convert the string to a byte array and store it in the session
            var codeVerifierBytes = Encoding.UTF8.GetBytes(codeVerifier);
            context.Session.Set(CodeVerifierKey, codeVerifierBytes);
        }

        /// <summary>
        /// 使用 PKCE 驗證時，取得 `code_Verifier` 參數值，用於驗證授權碼請求的合法性。
        /// </summary>
        public string? GetCodeVerifier()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return null;

            if (context.Session.TryGetValue(CodeVerifierKey, out var codeVerifierBytes))
            {
                return Encoding.UTF8.GetString(codeVerifierBytes);
            }

            return null;
        }

        /// <summary>
        /// 移除 `code_Verifier` 參數值，通常在 PKCE 驗證完成後清除已使用的 `code_Verifier` 值。
        /// </summary>
        public void RemoveCodeVerifier()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return;

            context.Session.Remove(CodeVerifierKey);
        }
    }
}
