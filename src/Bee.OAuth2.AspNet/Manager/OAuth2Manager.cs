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
    public static class OAuth2Manager
    {
        /// <summary>
        /// 存放 OAuth2 用戶端的集合。
        /// </summary>
        private static Dictionary<string, TOAuth2Client> Clients { get; } = new Dictionary<string, TOAuth2Client>();
 
        /// <summary>
        /// 註冊 OAuth2 用戶端。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        /// <param name="client">OAuth2 用戶端。</param>
        public static void RegisterClient(string clientName, TOAuth2Client client)
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentException("Client name cannot be null or empty.", nameof(clientName));

            if (client == null)
                throw new ArgumentNullException(nameof(client), "OAuth2 client instance cannot be null.");

            Clients[clientName] = client;
        }

        /// <summary>
        /// 取得已註冊的 OAuth2 用戶端。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        public static TOAuth2Client GetClient(string clientName)
        {
            if (Clients.TryGetValue(clientName, out var client))
            {
                return client;
            }

            return null;
        }

        /// <summary>
        /// 取得 OAuth2 授權 URL。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        public static string GetAuthorizationUrl(string clientName)
        {
            var client = GetClient(clientName);
            var state = CryptoFunc.AesEncrypt(clientName);
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
                var clientName = CryptoFunc.AesDecrypt(state);
                var client = GetClient(clientName);
                if (!client.ValidateState(state))
                {
                    throw new InvalidOperationException("Invalid OAuth2 state.");
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
