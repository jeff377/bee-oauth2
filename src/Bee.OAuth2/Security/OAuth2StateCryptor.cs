using System;
using System.Text;
using Bee.Base;

namespace Bee.OAuth2
{
    /// <summary>
    /// 提供 OAuth2 state 加密與解密功能，使用 AES-CBC + HMAC 方式保護完整性。
    /// 加密內容為 clientName，可日後擴充為 JSON payload。
    /// </summary>
    public static class OAuth2StateCryptor
    {
        private static readonly byte[] combinedKey = GetCombinedKey();

        /// <summary>
        /// 取得組合的 AES 和 HMAC 金鑰。
        /// </summary>
        /// <remarks>
        /// 可使用 <see cref="AesCbcHmacKeyGenerator.GenerateBase64CombinedKey"/> 方法產生組合金鑰，
        /// 並將其設定為 <c>OAUTH2_STATE_KEY</c> 環境變數，用於後續加解密作業。
        /// </remarks>
        private static byte[] GetCombinedKey()
        {
            string base64 = Environment.GetEnvironmentVariable("OAUTH2_STATE_KEY");
            if (string.IsNullOrWhiteSpace(base64))
                throw new InvalidOperationException("Missing environment variable: OAUTH2_STATE_KEY");

            return Convert.FromBase64String(base64);
        }

        /// <summary>
        /// 將用戶端名稱加密為 state 字串。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        public static string EncryptClientName(string clientName)
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException(nameof(clientName));
                        
            AesCbcHmacKeyGenerator.FromCombinedKey(combinedKey, out var aesKey, out var hmacKey);
            var plainBytes = Encoding.UTF8.GetBytes(clientName);
            var cipherBytes = AesCbcHmacCryptor.Encrypt(plainBytes, aesKey, hmacKey);
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// 從 state 字串解密取得用戶端名稱，若驗證失敗將拋出例外。
        /// </summary>
        /// <param name="state">state 字串。</param>
        public static string DecryptClientName(string state)
        {
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentNullException(nameof(state));

            AesCbcHmacKeyGenerator.FromCombinedKey(combinedKey, out var aesKey, out var hmacKey);
            var cipherBytes = Convert.FromBase64String(state);
            var plainBytes = AesCbcHmacCryptor.Decrypt(cipherBytes, aesKey, hmacKey);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}

