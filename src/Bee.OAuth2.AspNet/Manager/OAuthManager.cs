using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Bee.Base;

namespace Bee.OAuth2.AspNet
{
    /// <summary>
    /// 提供 ASP.NET 程式 OAuth2 整合認證管理者。
    /// </summary>
    public static class OAuthManager
    {
        private static Dictionary<string, TOAuthClient> _Clients = null;

        /// <summary>
        /// 存放 OAuth2 用戶端的集合。
        /// </summary>
        private static Dictionary<string, TOAuthClient> Clients
        {
            get
            {
                if (BaseFunc.IsNull(_Clients))
                    _Clients = new Dictionary<string, TOAuthClient>();
                return _Clients;
            }
        }

        /// <summary>
        /// 註冊 OAuth2 用戶端。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        /// <param name="client">OAuth2 用戶端。</param>
        public static void RegisterClient(string clientName, TOAuthClient client)
        {
            if (string.IsNullOrEmpty(clientName) || client == null)
                throw new ArgumentNullException("Client name and instance cannot be null.");

            Clients[clientName] = client;
        }

        /// <summary>
        /// 取得已註冊的 OAuth2 用戶端。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        public static TOAuthClient GetClient(string clientName)
        {
            if (Clients.TryGetValue(clientName, out var client))
                return client;

            throw new KeyNotFoundException($"OAuth client '{clientName}' not found.");
        }

        /// <summary>
        /// 取得 OAuth2 授權 URL。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        public static string GetAuthorizationUrl(string clientName)
        {
            var client = GetClient(clientName);
            var state = EncryptionFunc.AesEncrypt(clientName);
            return client.GetAuthorizationUrl(state);
        }

        /// <summary>
        /// 轉向 OAuth2 授權 URL。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        public static void RedirectToAuthorization(string clientName)
        {
            var authUrl = GetAuthorizationUrl(clientName);
            HttpContext.Current.Response.Redirect(authUrl);
        }

        /// <summary>
        /// 驗證 OAuth2 回傳授權碼，並取得用戶資料。
        /// </summary>
        public static async Task<TAuthorizationResult> ValidateAuthorization()
        {
            string code = HttpContext.Current.Request.QueryString["code"];
            string state = HttpContext.Current.Request.QueryString["state"]; // OAuth2 回傳的 state

            try
            {
                var clientName = EncryptionFunc.AesDecrypt(state);
                var client = GetClient(clientName);
                if (!client.ValidateState(state))
                {
                    throw new Exception("Validate state error");
                }
                return await client.ValidateAuthorization(code);
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
