using System;
using System.Web;

namespace Bee.OAuth2.AspNet
{
    /// <summary>
    /// 提供 ASP.NET 程式的 OAuth2 驗證流程中的狀態儲存機制。
    /// </summary>
    public class StateStorage : IStateStorage
    {
        private const string StateKey = "_StateKey";
        private const string CodeVerifierKey = "_CodeVerifierKey";

        /// <summary>
        /// 儲存 `state` 參數值。
        /// </summary>
        /// <param name="value">儲存的狀態值，例如隨機產生的 `state` 字串。</param>
        public void SaveState(string value)
        {
            HttpCookie cookie = new HttpCookie(StateKey, value)
            {
                HttpOnly = true,  // 防止 JavaScript 存取，避免 XSS 攻擊
                Secure = true,  // 只允許 HTTPS 傳輸，避免中間人攻擊
                SameSite = SameSiteMode.None,  // 允許跨站傳遞（避免跨網站登入問題）
                Expires = DateTime.Now.Add(TimeSpan.FromMinutes(10))  // 設定有效時間為 10 分鐘
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 取得 `state` 參數值，用於驗證 OAuth2 callback 時返回的 `state` 是否一致。
        /// </summary>
        /// <returns>返回儲存的 `state` 值，如果不存在則回傳 `null`。</returns>
        public string GetState()
        {
            return HttpContext.Current.Request.Cookies[StateKey]?.Value;
        }

        /// <summary>
        /// 移除 `state` 參數值，通常在 OAuth2 驗證完成後清除已使用的 `state` 值。
        /// </summary>
        public void RemoveState()
        {
            if (HttpContext.Current.Request.Cookies[StateKey] != null)
            {
                HttpCookie cookie = new HttpCookie(StateKey) { Expires = DateTime.Now.AddDays(-1) };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 使用 PKCE 驗證時，儲存 `code_Verifier` 參數值。
        /// </summary>
        /// <param name="codeVerifier">用戶端隨機產生的 `code_Verifier`  字串。</param>
        public void SaveCodeVerifier(string codeVerifier)
        {
            HttpContext.Current.Session[CodeVerifierKey] = codeVerifier;
        }

        /// <summary>
        /// 使用 PKCE 驗證時，取得 `code_Verifier` 參數值，用於驗證授權碼請求的合法性。
        /// </summary>
        public string GetCodeVerifier()
        {
            return HttpContext.Current.Session[CodeVerifierKey] as string;
        }

        /// <summary>
        /// 移除 `code_Verifier` 參數值，通常在 PKCE 驗證完成後清除已使用的 `code_Verifier` 值。
        /// </summary>
        public void RemoveCodeVerifier()
        {
            HttpContext.Current.Session.Remove(CodeVerifierKey);
        }
    }
}
