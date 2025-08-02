using System;
using Newtonsoft.Json.Linq;

namespace Bee.OAuth2
{
    /// <summary>
    /// Auth0 OAuth2 驗證服務提供者，負責處理授權流程、交換 Access Token 及取得用戶資訊。
    /// </summary>
    public class Auth0OAuth2Provider : OAuth2Provider
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public Auth0OAuth2Provider(Auth0OAuth2Options options) : base(options)
        {
        }

        /// <summary>
        /// OAuth2 驗證服務提供者名稱。
        /// </summary>
        public override string ProviderName { get; } = "Auth0";

        /// <summary>
        /// 解析用戶資訊 JSON 字串。
        /// </summary>
        /// <param name="json">用戶資訊 JSON 字串。</param>
        public override UserInfo ParseUserJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json), "JSON string cannot be null or empty.");

            var jObject = JObject.Parse(json);

            return new UserInfo
            {
                UserId = jObject["sub"]?.ToString(),
                UserName = jObject["name"]?.ToString() ?? jObject["nickname"]?.ToString(),
                Email = jObject["email"]?.ToString(),
                RawJson = json
            };
        }
    }
}
