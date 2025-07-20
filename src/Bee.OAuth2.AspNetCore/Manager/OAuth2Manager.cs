using Microsoft.AspNetCore.Http;

namespace Bee.OAuth2.AspNetCore
{
    /// <summary>
    /// 提供 ASP.NET Core 程式 OAuth2 整合認證管理者。
    /// </summary>
    public class OAuth2Manager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Dictionary<string, OAuth2Client> Clients { get; } = new Dictionary<string, OAuth2Client>();

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="httpContextAccessor">提供目前 HttpContext 的存取權。</param>
        public OAuth2Manager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 註冊 OAuth2 用戶端。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        /// <param name="client">OAuth2 用戶端。</param>
        public void RegisterClient(string clientName, OAuth2Client client)
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentException("Client name cannot be null or empty.", nameof(clientName));
            if (Clients.ContainsKey(clientName))
                throw new InvalidOperationException($"Client '{clientName}' is already registered.");
            Clients[clientName] = client ?? throw new ArgumentNullException(nameof(client), "OAuth2 client instance cannot be null.");
        }

        /// <summary>
        /// 取得已註冊的 OAuth2 用戶端。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        public OAuth2Client? GetClient(string clientName)
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
        public string GetAuthorizationUrl(string clientName)
        {
            var client = GetClient(clientName);
            if (client == null)
            {
                throw new InvalidOperationException($"Client '{clientName}' is not registered.");
            }
            var state = OAuth2StateCryptor.EncryptClientName(clientName);
            return client.GetAuthorizationUrl(state);
        }

        /// <summary>
        /// 轉向 OAuth2 授權 URL。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        public void RedirectToAuthorization(string clientName)
        {
            string authUrl = GetAuthorizationUrl(clientName);
            _httpContextAccessor.HttpContext.Response.Redirect(authUrl);
        }

        /// <summary>
        /// 驗證 OAuth2 回傳授權碼，並取得用戶資料。
        /// </summary>
        public async Task<AuthorizationResult> ValidateAuthorization()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                return new AuthorizationResult()
                {
                    IsSuccess = false,
                    Exception = new InvalidOperationException("HttpContext or Request is null.")
                };
            }

            string? code = request.Query["code"];
            string? state = request.Query["state"]; // OAuth2 回傳的 state

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
            {
                return new AuthorizationResult()
                {
                    IsSuccess = false,
                    Exception = new InvalidOperationException("Authorization code or state is missing.")
                };
            }

            try
            {
                var clientName = OAuth2StateCryptor.DecryptClientName(state);
                var client = GetClient(clientName);
                if (client == null || !client.ValidateState(state))
                {
                    throw new InvalidOperationException("Invalid OAuth2 state or client not found.");
                }
                return await client.ValidateAuthorization(code);
            }
            catch (Exception ex)
            {
                return new AuthorizationResult()
                {
                    IsSuccess = false,
                    Exception = ex
                };
            }
        }
    }
}
