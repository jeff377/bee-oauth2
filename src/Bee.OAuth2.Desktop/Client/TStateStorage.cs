namespace Bee.OAuth2.Desktop
{
    /// <summary>
    /// 提供 WInForms 程式的 OAuth2 驗證流程中的狀態儲存機制。
    /// </summary>
    public class TStateStorage : IStateStorage
    {
        /// <summary>
        /// OAuth2 驗證流程的 `state` 參數值。
        /// </summary>
        private string State { get; set; } = string.Empty;

        /// <summary>
        /// OAuth2 驗證流程的 `code_Verifier` 參數值。
        /// </summary>
        private string CodeVerifier { get; set; } = string.Empty;

        /// <summary>
        /// 儲存 `state` 參數值。
        /// </summary>
        /// <param name="value">儲存的狀態值，例如隨機產生的 `state` 字串。</param>
        public void SaveState(string value)
        {
            this.State = value;
        }

        /// <summary>
        /// 取得 `state` 參數值，用於驗證 OAuth2 callback 時返回的 `state` 是否一致。
        /// </summary>
        /// <returns>返回儲存的 `state` 值，如果不存在則回傳 `null`。</returns>
        public string GetState()
        {
            return this.State;
        }

        /// <summary>
        /// 移除 `state` 參數值，通常在 OAuth2 驗證完成後清除已使用的 `state` 值。
        /// </summary>
        public void RemoveState()
        {
            this.State = string.Empty;
        }

        /// <summary>
        /// 使用 PKCE 驗證時，儲存 `code_Verifier` 參數值。
        /// </summary>
        /// <param name="codeVerifier">用戶端隨機產生的 `code_Verifier`  字串。</param>
        public void SaveCodeVerifier(string codeVerifier)
        {
            this.CodeVerifier = codeVerifier;
        }

        /// <summary>
        /// 使用 PKCE 驗證時，取得 `code_Verifier` 參數值，用於驗證授權碼請求的合法性。
        /// </summary>
        public string GetCodeVerifier()
        {
            return this.CodeVerifier;
        }

        /// <summary>
        /// 移除 `code_Verifier` 參數值，通常在 PKCE 驗證完成後清除已使用的 `code_Verifier` 值。
        /// </summary>
        public void RemoveCodeVerifier()
        {
            this.CodeVerifier = string.Empty;
        }
    }
}
