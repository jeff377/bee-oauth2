namespace Bee.OAuth2.Desktop
{
    /// <summary>
    /// 提供 WInForms 程式 OAuth2 整合認證管理者。
    /// </summary>
    public static class OAuth2Manager
    {
        /// <summary>
        /// 存放 OAuth2 用戶端的集合。
        /// </summary>
        private static Dictionary<string, OAuth2Client> Clients { get; } = new Dictionary<string, OAuth2Client>();

        /// <summary>
        /// 註冊 OAuth2 用戶端。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        /// <param name="client">OAuth2 用戶端。</param>
        public static void RegisterClient(string clientName, OAuth2Client client)
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
        public static OAuth2Client? GetClient(string clientName)
        {
            if (Clients.TryGetValue(clientName, out var client))
            {
                return client;
            }

            return null;
        }

        /// <summary>
        /// 執行登入。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        public static Task<AuthorizationResult> Login(string clientName)
        {
            var client = GetClient(clientName);
            if (client == null)
                throw new InvalidOperationException($"OAuth client not found: {clientName}");

            return client.Login();  
        }

    }
}
