using System;
using System.Security.Cryptography;
using System.Text;

namespace Bee.OAuth2
{
    /// <summary>
    /// PKCE (Proof Key for Code Exchange) 輔助工具類別，
    /// 用於產生 `code_verifier` 和 `code_challenge`，以提高 OAuth2 授權碼流程的安全性。
    /// </summary>
    public static class PkceHelper
    {
        /// <summary>
        /// 產生隨機的 `code_verifier`。
        /// `code_verifier` 是一個高熵的隨機字串，
        /// 長度介於 43 到 128 字元之間，只包含 URL 安全的字元。
        /// </summary>
        /// <returns>產生的 `code_verifier` 字串</returns>
        public static string GenerateCodeVerifier()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[32]; // 產生 32 位元組的隨機數據 (256-bit)
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes)
                    .TrimEnd('=')  // 移除 Base64 可能產生的 '=' 補位字元
                    .Replace('+', '-') // 轉換 '+' 為 URL 安全的 '-'
                    .Replace('/', '_'); // 轉換 '/' 為 URL 安全的 '_'
            }
        }

        /// <summary>
        /// 使用 SHA256 產生 `code_challenge`。
        /// `code_challenge` 是 `code_verifier` 的 SHA256 雜湊值，並轉換為 Base64 URL 編碼格式。
        /// </summary>
        /// <param name="codeVerifier">原始的 `code_verifier`</param>
        /// <returns>對應的 `code_challenge` 字串</returns>
        public static string GenerateCodeChallenge(string codeVerifier)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.ASCII.GetBytes(codeVerifier);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash)
                    .TrimEnd('=')  // 移除 Base64 可能產生的 '=' 補位字元
                    .Replace('+', '-') // 轉換 '+' 為 URL 安全的 '-'
                    .Replace('/', '_'); // 轉換 '/' 為 URL 安全的 '_'
            }
        }
    }

}
