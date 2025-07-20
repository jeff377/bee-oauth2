namespace Bee.OAuth2
{
    /// <summary>
    /// 用戶資料。
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用戶帳號。
        /// </summary>
        public string UserId { get; protected internal set; }

        /// <summary>
        /// 用戶名稱。
        /// </summary>
        public string UserName { get; protected internal set; }

        /// <summary>
        /// 電子郵件。
        /// </summary>
        public string Email { get; protected internal set; }

        /// <summary>
        /// 原始的 JSON 資料)。
        /// </summary>
        public string RawJson { get; protected internal set; }
    }
}
