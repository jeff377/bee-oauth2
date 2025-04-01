using System;
using System.Threading.Tasks;
using Bee.Base;

namespace Bee.OAuth2
{
    /// <summary>
    /// 提供 OAuth2 驗證的基本功能，適用於不同平台的擴充。
    /// </summary>
    public abstract class TBaseOAuthClient
    {
        private IOAuthProvider _provider = null;
        private bool _usePKCE = false;

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public TBaseOAuthClient(TOAuthOptions options)
        {
            _usePKCE = options.UsePKCE;
            if (options is TGoogleOAuthOptions)
                _provider = new TGoogleOAuthProvider(options as TGoogleOAuthOptions);
            else if (options is TLineOAuthOptions)
                _provider = new TLineOAuthProvider(options as TLineOAuthOptions);
            else if (options is TAzureOAuthOptions)
                _provider = new TAzureOAuthProvider(options as TAzureOAuthOptions);
            else if (options is TFacebookOAuthOptions)
                _provider = new TFacebookOAuthProvider(options as TFacebookOAuthOptions);
            else
                throw new NotSupportedException();
        }

        /// <summary>
        /// OAuth2 驗證服務提供者。
        /// </summary>
        public IOAuthProvider Provider
        {
            get { return _provider; }
        }

        /// <summary>
        /// OAuth2 驗證流程中的狀態儲存機制。
        /// </summary>
        public abstract IStateStorage StateStorage
        {
            get;
        }

        /// <summary>
        /// 是否使用 PKCE 驗證。
        /// </summary>
        public bool UsePKCE
        {
            get { return _usePKCE; }
        }

        /// <summary>
        /// 產生 OAuth2 授權 URL，讓使用者登入並授權應用程式。
        /// </summary>
        /// <param name="state">用於防止 CSRF 的隨機字串</param>
        public string GetAuthorizationUrl(string state)
        {
            string sCodeVerifier, sCodeChallenge;

            this.StateStorage.SaveState(state);

            if (this.UsePKCE)
            {
                // 產生 PKCE 驗證碼
                sCodeVerifier = PKCEHelper.GenerateCodeVerifier();
                sCodeChallenge = PKCEHelper.GenerateCodeChallenge(sCodeVerifier);
                this.StateStorage.SaveCodeVerifier(sCodeVerifier);
                return this.Provider.GetAuthorizationUrl(state, sCodeChallenge);
            }
            else
            {
                return this.Provider.GetAuthorizationUrl(state);
            }
        }

        /// <summary>
        /// 檢查回傳的 `state` 是否有效，避免 CSRF 攻擊。
        /// </summary>
        /// <param name="returnedState">回傳的狀態碼。</param>
        public bool ValidateState(string returnedState)
        {
            string storedState = this.StateStorage.GetState();
            bool isValid = returnedState == storedState;
            this.StateStorage.RemoveState();
            return isValid;
        }

        /// <summary>
        /// 透過授權碼 (Authorization Code) 交換 Access Token。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼</param>
        public async Task<string> GetAccessTokenAsync(string authorizationCode)
        {
            string sCodeVerifier, sAccessToken;

            // 透過授權碼交換 Access Token
            if (this.UsePKCE)
            {
                sCodeVerifier = this.StateStorage.GetCodeVerifier();
                sAccessToken = await this.Provider.GetAccessTokenAsync(authorizationCode, sCodeVerifier);
                this.StateStorage.RemoveCodeVerifier();
            }
            else
            {
                sAccessToken = await this.Provider.GetAccessTokenAsync(authorizationCode);
            }
            return sAccessToken;
        }

        /// <summary>
        /// 透過 Access Token 取得用戶資訊。
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <returns>用戶資訊 JSON 字串</returns>
        public Task<string> GetUserInfoAsync(string accessToken)
        {
            return this.Provider.GetUserInfoAsync(accessToken);
        }

        /// <summary>
        /// 透過授權碼交換 Access Token，並取得用戶資訊。
        /// </summary>
        /// <param name="authorizationCode">回傳的授權碼。</param>
        /// <returns>授權碼取得相關資訊的回傳結果。</returns>
        public async Task<TAuthorizationResult> ValidateAuthorization(string authorizationCode)
        {
            string sAccessToken, sUserInfo;

            try
            {
                if (StrFunc.IsEmpty(authorizationCode))
                    throw new Exception("Authorization code is empty.");

                // 透過授權碼交換 Access Token
                sAccessToken = await GetAccessTokenAsync(authorizationCode);
                if (string.IsNullOrEmpty(sAccessToken))
                    throw new Exception("Access token not found in response.");

                // 透過 Access Token 取得用戶資訊
                sUserInfo = await GetUserInfoAsync(sAccessToken);

                // 回傳結果
                return new TAuthorizationResult()
                {
                    ProviderName = this.Provider.ProviderName,
                    IsSuccess = true,
                    AccessToken = sAccessToken,
                    UserInfo = this.Provider.ParseUserJson(sUserInfo)
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
