﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Bee.OAuth2
{
    /// <summary>
    /// Google OAuth2 驗證服務提供者，負責處理授權流程、交換 Access Token 及取得用戶資訊。
    /// </summary>
    public class GoogleOAuth2Provider : OAuth2Provider
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public GoogleOAuth2Provider(GoogleOAuth2Options options) : base(options)
        {
        }

        /// <summary>
        /// OAuth2 驗證服務提供者名稱。
        /// </summary>
        public override string ProviderName { get; } = "Google";

        /// <summary>
        /// 取得 Access Token 的參數集合。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼 (Authorization Code)。</param>
        /// <param name="codeVerifier">使用 PKCE 驗證時， 需傳入 `code_verifier` 參數值。</param>
        protected override Dictionary<string, string> GetAccessTokenParams(string authorizationCode, string codeVerifier = "")
        {
            var requestParams = base.GetAccessTokenParams(authorizationCode, codeVerifier);
            requestParams["client_secret"] = Options.ClientSecret; // Google 需要 client_secret
            return requestParams;
        }

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
                UserName = jObject["name"]?.ToString(),
                Email = jObject["email"]?.ToString(),
                RawJson = json
            };
        }
    }

}
