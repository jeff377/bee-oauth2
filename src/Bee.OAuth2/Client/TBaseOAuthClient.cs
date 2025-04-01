﻿using System;
using System.Threading.Tasks;
using Bee.Base;

namespace Bee.OAuth2
{
    /// <summary>
    /// 提供 OAuth2 驗證的基本功能，適用於不同平台的擴充。
    /// </summary>
    public abstract class TBaseOAuthClient
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public TBaseOAuthClient(TOAuthOptions options)
        {
            UsePKCE = options.UsePKCE;
            Provider = CreateProvider(options);
        }

        /// <summary>
        /// 建立 OAuth2 驗證服務提供者。  
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        private IOAuthProvider CreateProvider(TOAuthOptions options)
        {
            switch (options)
            {
                case TGoogleOAuthOptions googleOptions:
                    return new TGoogleOAuthProvider(googleOptions);
                case TLineOAuthOptions lineOptions:
                    return new TLineOAuthProvider(lineOptions);
                case TAzureOAuthOptions azureOptions:
                    return new TAzureOAuthProvider(azureOptions);
                case TFacebookOAuthOptions facebookOptions:
                    return new TFacebookOAuthProvider(facebookOptions);
                default:
                    throw new NotSupportedException("Unsupported OAuth provider.");
            }
        }

        /// <summary>
        /// OAuth2 驗證服務提供者。
        /// </summary>
        public IOAuthProvider Provider { get; private set; }

        /// <summary>
        /// OAuth2 驗證流程中的狀態儲存機制。
        /// </summary>
        public abstract IStateStorage StateStorage { get; }

        /// <summary>
        /// 是否使用 PKCE 驗證。
        /// </summary>
        public bool UsePKCE { get; private set; }

        /// <summary>
        /// 產生 OAuth2 授權 URL，讓使用者登入並授權應用程式。
        /// </summary>
        /// <param name="state">用於防止 CSRF 的隨機字串</param>
        public string GetAuthorizationUrl(string state)
        {
            // 儲存 `state` 資料，暫存 OAuth2 流程中的 `state` 參數，以便後續驗證
            StateStorage.SaveState(state);

            if (UsePKCE)
            {
                // 產生 PKCE 驗證碼
                string codeVerifier = PKCEHelper.GenerateCodeVerifier();
                string codeChallenge = PKCEHelper.GenerateCodeChallenge(codeVerifier);
                // 儲存 `code_Verifier` 資料，暫存 PKCE 驗證的 `code_Verifier` 參數，以便後續驗證
                StateStorage.SaveCodeVerifier(codeVerifier);
                return Provider.GetAuthorizationUrl(state, codeChallenge);
            }
            else
            {
                return Provider.GetAuthorizationUrl(state);
            }
        }

        /// <summary>
        /// 檢查回傳的 `state` 是否有效，避免 CSRF 攻擊。
        /// </summary>
        /// <param name="returnedState">回傳的狀態碼。</param>
        public bool ValidateState(string returnedState)
        {
            // 取得指定 `state` 的值，用於驗證 OAuth2 callback 時返回的 `state` 是否一致
            string storedState = StateStorage.GetState();
            StateStorage.RemoveState();
            return  returnedState == storedState;
        }

        /// <summary>
        /// 透過授權碼 (Authorization Code) 交換 Access Token。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼</param>
        public async Task<string> GetAccessTokenAsync(string authorizationCode)
        {
            // 透過授權碼交換 Access Token
            if (UsePKCE)
            {
                // 取得 `code_Verifier` 的值，用於驗證授權碼請求的合法性
                string codeVerifier = StateStorage.GetCodeVerifier();
                StateStorage.RemoveCodeVerifier();
                return await Provider.GetAccessTokenAsync(authorizationCode, codeVerifier);                
            }
            else
            {
                return  await Provider.GetAccessTokenAsync(authorizationCode);
            }
        }

        /// <summary>
        /// 透過 Access Token 取得用戶資訊。
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <returns>用戶資訊 JSON 字串</returns>
        public Task<string> GetUserInfoAsync(string accessToken)
        {
            return Provider.GetUserInfoAsync(accessToken);
        }

        /// <summary>
        /// 透過授權碼交換 Access Token，並取得用戶資訊。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼。</param>
        /// <returns>授權碼取得相關資訊的回傳結果。</returns>
        public async Task<TAuthorizationResult> ValidateAuthorization(string authorizationCode)
        {
            try
            {
                if (StrFunc.IsEmpty(authorizationCode))
                    throw new Exception("Authorization code is empty.");

                // 透過授權碼交換 Access Token
                string accessToken = await GetAccessTokenAsync(authorizationCode);
                if (string.IsNullOrEmpty(accessToken))
                    throw new Exception("Access token not found in response.");

                // 透過 Access Token 取得用戶資訊
                string userInfo = await GetUserInfoAsync(accessToken);

                // 回傳結果
                return new TAuthorizationResult()
                {
                    ProviderName = Provider.ProviderName,
                    IsSuccess = true,
                    AccessToken = accessToken,
                    UserInfo = Provider.ParseUserJson(userInfo)
                };
            }
            catch (Exception ex)
            {
                return new TAuthorizationResult()
                {
                    IsSuccess = false,
                    Exception = ex
                };
            }
        }
    }
}
