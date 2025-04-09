using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Bee.OAuth2
{
    /// <summary>
    /// Facebook OAuth2 驗證服務提供者，負責處理授權流程、交換 Access Token 及取得用戶資訊。
    /// </summary>
    public class TFacebookOAuth2Provider : TOAuth2Provider
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public TFacebookOAuth2Provider(TFacebookOAuth2Options options) : base(options)
        {
        }

        /// <summary>
        /// OAuth2 驗證服務提供者名稱。
        /// </summary>
        public override string ProviderName { get; } = "Facebook";

        /// <summary>
        /// 取得 OAuth2 授權 URL 的參數集合。
        /// </summary>
        /// <param name="state">用於防止 CSRF 的隨機字串</param>
        /// <param name="codeChallenge">使用 PKCE 驗證時， 需傳入 `code_challenge` 參數值。</param>
        protected override Dictionary<string, string> GetAuthorizationUrlParams(string state, string codeChallenge = "")
        {
            var queryParams = base.GetAuthorizationUrlParams(state, codeChallenge);
            queryParams["scope"] = string.Join(",", Options.Scopes);  // Facebook 的 scope 以逗號分隔
            return queryParams;
        }

        /// <summary>
        /// 取得用戶資訊的 URL，預設為 `UserInfoEndpoint`。
        /// </summary>
        protected override string GetUserInfoUrl()
        {
            var fields = "id,name,email,picture";
            return $"{Options.UserInfoEndpoint}?fields={Uri.EscapeDataString(fields)}";
        }

        /// <summary>
        /// 解析用戶資訊 JSON 字串。
        /// </summary>
        /// <param name="json">用戶資訊 JSON 字串。</param>
        public override TUserInfo ParseUserJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json), "JSON string cannot be null or empty.");

            var jObject = JObject.Parse(json);

            return new TUserInfo
            {
                UserId = jObject["id"]?.ToString(),
                UserName = jObject["name"]?.ToString(),
                Email = jObject["email"]?.ToString(),
                RawJson = json
            };
        }

        /// <summary>
        /// 使用 Refresh Token 取得新的 Access Token。
        /// </summary>
        /// <param name="refreshToken">Refresh Token。</param>
        /// <returns>新的 Access Token</returns>
        public override Task<string> RefreshAccessTokenAsync(string refreshToken)
        {
            throw new NotSupportedException();  // Facebook 不支援 Refresh Token
        }
    }

}
