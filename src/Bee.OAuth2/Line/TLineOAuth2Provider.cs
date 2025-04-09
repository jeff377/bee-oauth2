using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Bee.Base;
using System.Linq;

namespace Bee.OAuth2
{
    /// <summary>
    /// LINE OAuth2 驗證服務提供者，負責處理授權流程、交換 Access Token 及取得用戶資訊。
    /// </summary>
    public class TLineOAuth2Provider : IOAuth2Provider
    {
        private readonly HttpClient _HttpClient = new HttpClient();

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public TLineOAuth2Provider(TLineOAuth2Options options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// OAuth2 驗證服務提供者名稱。
        /// </summary>
        public string ProviderName { get; } = "LINE";

        /// <summary>
        /// OAuth2 設定選項。
        /// </summary>
        public TLineOAuth2Options Options { get; private set; }

        /// <summary>
        /// 產生 LINE OAuth2 授權 URL，讓使用者登入並授權應用程式。
        /// </summary>
        /// <param name="state">用於防止 CSRF 的隨機字串</param>
        /// <param name="codeChallenge">使用 PKCE 驗證時，需傳入 `code_challenge` 參數值。</param>
        /// <returns>OAuth2 授權 URL</returns>
        public string GetAuthorizationUrl(string state, string codeChallenge = "")
        {
            var queryParams = new Dictionary<string, string>
            {
                { "client_id", Options.ClientId },
                { "redirect_uri", Options.RedirectUri },
                { "response_type", "code" },
                { "scope", string.Join(" ", Options.Scopes) },
                { "state", state }
            };

            if (StrFunc.IsNotEmpty(codeChallenge))
            {
                queryParams["code_challenge"] = codeChallenge;
                queryParams["code_challenge_method"] = "S256"; // 必須指定 S256 方法
            }

            // 使用 `HttpUtility.ParseQueryString` 或 `string.Join` 來組合 URL 參數，確保正確編碼
            string queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            return $"{Options.AuthorizationEndpoint}?{queryString}";
        }

        /// <summary>
        /// 取得 OAuth2 驗證流程完成後的回呼網址。
        /// </summary>
        public string GetRedirectUrl()
        {
            return Options.RedirectUri;
        }

        /// <summary>
        /// 透過授權碼 (Authorization Code) 交換 Access Token。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼 (Authorization Code)。</param>
        /// <param name="codeVerifier">使用 PKCE 驗證時，需傳入 `code_verifier` 參數值。</param>
        /// <returns>Access Token</returns>
        public async Task<string> GetAccessTokenAsync(string authorizationCode, string codeVerifier = "")
        {
            // 使用 Dictionary 簡化參數組合
            var requestParams = new Dictionary<string, string>
            {
                { "client_id", Options.ClientId },
                { "client_secret", Options.ClientSecret },
                { "redirect_uri", Options.RedirectUri },
                { "code", authorizationCode },
                { "grant_type", "authorization_code" }
            };

            if (StrFunc.IsNotEmpty(codeVerifier))
            {
                requestParams["code_verifier"] = codeVerifier; // PKCE 驗證
            }

            using (var requestBody = new FormUrlEncodedContent(requestParams))
            using (var response = await _HttpClient.PostAsync(Options.TokenEndpoint, requestBody).ConfigureAwait(false))
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to obtain access token. Status: {response.StatusCode}, Response: {responseContent}");
                }

                var tokenData = JObject.Parse(responseContent);
                return tokenData["access_token"]?.ToString() ?? throw new Exception("Access token not found in response.");
            }
        }

        /// <summary>
        /// 透過 Access Token 取得用戶資訊。
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <returns>用戶資訊 JSON 字串</returns>
        public async Task<string> GetUserInfoAsync(string accessToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInfoEndpoint))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await _HttpClient.SendAsync(request).ConfigureAwait(false))
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException($"Failed to retrieve user information. Status: {response.StatusCode}, Response: {responseContent}");
                    }

                    return responseContent;
                }
            }
        }

        /// <summary>
        /// 解析用戶資訊 JSON 字串。
        /// </summary>
        /// <param name="json">用戶資訊 JSON 字串。</param>
        public TUserInfo ParseUserJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json), "JSON string cannot be null or empty.");

            var jObject = JObject.Parse(json);

            return new TUserInfo
            {
                UserId = jObject["userId"]?.ToString(),
                UserName = jObject["displayName"]?.ToString(),
                Email = jObject["email"]?.ToString(), // LINE API 可能不會提供 email
                RawJson = json
            };
        }

        /// <summary>
        /// 使用 Refresh Token 取得新的 Access Token。
        /// </summary>
        /// <param name="refreshToken">Refresh Token</param>
        /// <returns>新的 Access Token</returns>
        public async Task<string> RefreshAccessTokenAsync(string refreshToken)
        {
            var requestBody = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", Options.ClientId),
            new KeyValuePair<string, string>("client_secret", Options.ClientSecret),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
            new KeyValuePair<string, string>("grant_type", "refresh_token")
        });

            var response = await _HttpClient.PostAsync(Options.TokenEndpoint, requestBody).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to refresh access token.");
            }

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var tokenData = JObject.Parse(json);
            return tokenData["access_token"]?.ToString() ?? throw new Exception("Access token not found in response.");
        }
    }

}
