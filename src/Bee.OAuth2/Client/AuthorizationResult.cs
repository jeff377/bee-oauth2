using System;

namespace Bee.OAuth2
{
    /// <summary>
    /// 授權碼取得相關資訊的回傳結果。
    /// </summary>
    public class AuthorizationResult
    {
        /// <summary>
        /// OAuth2 驗證服務提供者名稱。
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 是否成功。
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 用戶資料。
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// OAuth2 Access Token。
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 例外錯誤。
        /// </summary>
        public Exception Exception { get; set; }
    }
}
