﻿using System;

namespace Bee.OAuth2.WinForms
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
        /// 儲存 `state` 資料，例如 OAuth2 流程中的 `state` 參數，以便後續驗證。
        /// </summary>
        /// <param name="value">儲存的狀態值，例如隨機產生的 `state` 字串。</param>
        public void SaveState(string value)
        {
            this.State = value;
        }

        /// <summary>
        /// 取得指定 `state` 的值，用於驗證 OAuth2 callback 時返回的 `state` 是否一致。
        /// </summary>
        public string GetState()
        {
            return this.State;
        }

        /// <summary>
        /// 移除指定 `state`，通常在 OAuth2 驗證完成後清除已使用的 `state` 值。
        /// </summary>
        public void RemoveState()
        {
            this.State = string.Empty;
        }

        /// <summary>
        /// 儲存 PKCE 驗證流程的 `code_Verifier` 資料。
        /// </summary>
        /// <param name="codeVerifier">用戶端隨機產生的 `code_Verifier`  字串。</param>
        public void SaveCodeVerifier(string codeVerifier)
        {
            this.CodeVerifier = codeVerifier;
        }

        /// <summary>
        /// 取得 `code_Verifier` 的值，用於驗證授權碼請求的合法性。
        /// </summary>
        public string GetCodeVerifier()
        {
            return this.CodeVerifier;
        }

        /// <summary>
        /// 移除 `code_Verifier` 資料，通常在 OAuth2 取得 Access Token 後清除。
        /// </summary>
        public void RemoveCodeVerifier()
        {
            this.CodeVerifier = string.Empty;
        }
    }
}
