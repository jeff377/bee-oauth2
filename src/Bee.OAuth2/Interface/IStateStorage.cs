using System;

namespace Bee.OAuth2
{
    /// <summary>
    /// 定義 OAuth2 驗證流程中的狀態儲存機制，例如使用 Cookie、Session 或資料庫來存取授權流程的 `state`  及 `code_Verifier` 參數。
    /// </summary>
    public interface IStateStorage
    {
        /// <summary>
        /// 儲存 `state` 參數值。
        /// </summary>
        /// <param name="value">儲存的狀態值，例如隨機產生的 `state` 字串。</param>
        void SaveState(string value);

        /// <summary>
        /// 取得 `state` 參數值，用於驗證 OAuth2 callback 時返回的 `state` 是否一致。
        /// </summary>
        /// <returns>返回儲存的 `state` 值，如果不存在則回傳 `null`。</returns>
        string GetState();

        /// <summary>
        /// 移除 `state` 參數值，通常在 OAuth2 驗證完成後清除已使用的 `state` 值。
        /// </summary>
        void RemoveState();

        /// <summary>
        /// 使用 PKCE 驗證時，儲存 `code_Verifier` 參數值。
        /// </summary>
        /// <param name="codeVerifier">用戶端隨機產生的 `code_Verifier`  字串。</param>
        void SaveCodeVerifier(string codeVerifier);

        /// <summary>
        /// 使用 PKCE 驗證時，取得 `code_Verifier` 參數值，用於驗證授權碼請求的合法性。
        /// </summary>
        string GetCodeVerifier();

        /// <summary>
        /// 移除 `code_Verifier` 參數值，通常在 PKCE 驗證完成後清除已使用的 `code_Verifier` 值。
        /// </summary>
        void RemoveCodeVerifier();
    }

}
