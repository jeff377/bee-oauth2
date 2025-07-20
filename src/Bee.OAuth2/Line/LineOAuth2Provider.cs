using System;
using Newtonsoft.Json.Linq;

namespace Bee.OAuth2
{
    /// <summary>
    /// LINE OAuth2 驗證服務提供者，負責處理授權流程、交換 Access Token 及取得用戶資訊。
    /// </summary>
    public class LineOAuth2Provider : OAuth2Provider
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public LineOAuth2Provider(LineOAuth2Options options) : base(options)
        {
        }

        /// <summary>
        /// OAuth2 驗證服務提供者名稱。
        /// </summary>
        public override string ProviderName { get; } = "LINE";

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
                UserId = jObject["userId"]?.ToString(),
                UserName = jObject["displayName"]?.ToString(),
                Email = jObject["email"]?.ToString(), // LINE API 可能不會提供 email
                RawJson = json
            };
        }
    }

}
