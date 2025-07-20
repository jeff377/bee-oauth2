using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bee.Base;
using Newtonsoft.Json.Linq;

namespace Bee.OAuth2
{
    /// <summary>
    /// OAuth2 驗證服務提供者基底類別，負責處理授權流程、交換 Access Token 及取得用戶資訊。
    /// </summary>
    public abstract class OAuth2Provider : IOAuth2Provider
    {
        /// <summary>
        /// HttpClient 實例，用於發送 HTTP 請求。
        /// </summary>
        protected readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public OAuth2Provider(OAuth2Options options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            Options = options;
        }

        /// <summary>
        /// OAuth2 驗證服務提供者名稱。
        /// </summary>
        public abstract string ProviderName { get; }

        /// <summary>
        /// OAuth2 設定選項。
        /// </summary>
        public OAuth2Options Options { get; private set; }

        /// <summary>
        /// 取得 OAuth2 授權 URL 的參數集合。
        /// </summary>
        /// <param name="state">用於防止 CSRF 的隨機字串</param>
        /// <param name="codeChallenge">使用 PKCE 驗證時， 需傳入 `code_challenge` 參數值。</param>
        protected virtual Dictionary<string, string> GetAuthorizationUrlParams(string state, string codeChallenge = "")
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
            return queryParams;
        }

        /// <summary>
        /// 產生 OAuth2 授權 URL，讓使用者登入並授權應用程式。
        /// </summary>
        /// <param name="state">用於防止 CSRF 的隨機字串</param>
        /// <param name="codeChallenge">使用 PKCE 驗證時， 需傳入 `code_challenge` 參數值。</param>
        /// <returns>OAuth2 授權 URL</returns>
        public virtual string GetAuthorizationUrl(string state, string codeChallenge = "")
        {
            // 取得 OAuth2 授權 URL 的參數集合
            var queryParams = GetAuthorizationUrlParams(state, codeChallenge);
            // 使用 `HttpUtility.ParseQueryString` 或 `string.Join` 來組合 URL 參數，確保正確編碼
            string queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            return $"{Options.AuthorizationEndpoint}?{queryString}";
        }

        /// <summary>
        /// 取得 OAuth2 驗證流程完成後的回呼網址。
        /// </summary>
        public virtual string GetRedirectUrl()
        {
            return Options.RedirectUri;
        }

        /// <summary>
        /// 取得 Access Token 的參數集合。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼 (Authorization Code)。</param>
        /// <param name="codeVerifier">使用 PKCE 驗證時， 需傳入 `code_verifier` 參數值。</param>
        protected virtual Dictionary<string, string> GetAccessTokenParams(string authorizationCode, string codeVerifier = "")
        {
            // 使用 Dictionary 簡化參數組合
            var requestParams = new Dictionary<string, string>
            {
                { "client_id", Options.ClientId },
                { "redirect_uri", Options.RedirectUri },
                { "code", authorizationCode },
                { "grant_type", "authorization_code" }
            };

            if (StrFunc.IsNotEmpty(codeVerifier))
            {
                requestParams["code_verifier"] = codeVerifier;
            }
            else
            {
                requestParams["client_secret"] = Options.ClientSecret;
            }
            return requestParams;
        }

        /// <summary>
        /// 透過授權碼 (Authorization Code) 交換 Access Token。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼 (Authorization Code)。</param>
        /// <param name="codeVerifier">使用 PKCE 驗證時， 需傳入 `code_verifier` 參數值。</param>
        /// <returns>Access Token</returns>
        public virtual async Task<string> GetAccessTokenAsync(string authorizationCode, string codeVerifier = "")
        {
            // 取得 Access Token 的參數集合
            var requestParams = GetAccessTokenParams(authorizationCode, codeVerifier);

            using (var requestBody = new FormUrlEncodedContent(requestParams))
            using (var response = await _httpClient.PostAsync(Options.TokenEndpoint, requestBody).ConfigureAwait(false))
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
        /// 取得用戶資訊的 URL，預設為 `UserInfoEndpoint`。
        /// </summary>
        protected virtual string GetUserInfoUrl()
        {
            return Options.UserInfoEndpoint;
        }

        /// <summary>
        /// 透過 Access Token 取得用戶資訊。
        /// </summary>
        /// <param name="accessToken">Access Token。</param>
        /// <returns>用戶資訊 JSON 字串</returns>
        /// <exception cref="ArgumentNullException">當 `accessToken` 為空時拋出</exception>
        /// <exception cref="HttpRequestException">當請求失敗時拋出</exception>
        public virtual async Task<string> GetUserInfoAsync(string accessToken)
        {
            string url = GetUserInfoUrl();   // 取得用戶資訊的 URL
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await _httpClient.SendAsync(request).ConfigureAwait(false))
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
        public abstract UserInfo ParseUserJson(string json);

        /// <summary>
        /// 取得 Refresh Token 的參數集合。
        /// </summary>
        /// <param name="refreshToken"></param>
        protected virtual Dictionary<string, string> GetRefreshAccessTokenParams(string refreshToken)
        {
            return new Dictionary<string, string>
            {
                { "client_id", Options.ClientId },
                { "client_secret", Options.ClientSecret },
                { "refresh_token", refreshToken },
                { "grant_type", "refresh_token" }
            };
        }

        /// <summary>
        /// 使用 Refresh Token 取得新的 Access Token。
        /// </summary>
        /// <param name="refreshToken">Refresh Token</param>
        /// <returns>新的 Access Token</returns>
        public virtual async Task<string> RefreshAccessTokenAsync(string refreshToken)
        {
            // 取得 Refresh Token 的參數集合
            var parameters = GetRefreshAccessTokenParams(refreshToken);

            var requestBody = new FormUrlEncodedContent(parameters);

            using (var response = await _httpClient.PostAsync(Options.TokenEndpoint, requestBody).ConfigureAwait(false))
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to refresh access token. Status: {response.StatusCode}, Response: {content}");
                }

                var tokenData = JObject.Parse(content);
                var accessToken = tokenData["access_token"]?.ToString();

                if (string.IsNullOrEmpty(accessToken))
                    throw new Exception("Access token not found in response.");

                return accessToken;
            }
        }
    }
}
