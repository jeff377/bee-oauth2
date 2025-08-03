using Bee.OAuth2;
using Bee.OAuth2.Desktop;
using Newtonsoft.Json;

namespace OAuthDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = @"OAuthConfig.json";
            var config = LoadOAuthConfig(filePath);
            RegisterIfExists("Google", 600, 700, config?.GoogleOAuth);
            RegisterIfExists("Facebook", 900, 500, config?.FacebookOAuth);
            RegisterIfExists("Line", 600, 800, config?.LineOAuth);
            RegisterIfExists("Azure", 800, 600, config?.AzureOAuth);
            RegisterIfExists("Auth0", 800, 600, config?.Auth0OAuth);
        }

        private OAuthConfig LoadOAuthConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Configuration file not found.", filePath);

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<OAuthConfig>(json) ?? new OAuthConfig();
        }

        private void RegisterIfExists(string name, int width, int height, OAuth2Options? options)
        {
            if (options != null)
            {
                var client = new OAuth2Client(options)
                {
                    Caption = $"{name} Login",
                    Width = width,
                    Height = height
                };
                OAuth2Manager.RegisterClient(name, client);
            }
        }

        /// <summary>
        /// 顯示 OAuth2 整合認證回傳結果。
        /// </summary>
        /// <param name="result">授權碼取得相關資訊的回傳結果。</param>
        private void ShowResult(AuthorizationResult result)
        {
            if (result.Exception != null)
            {
                edtUserInfo.Text = result.Exception.Message;
                return;
            }

            var userInfo = result.UserInfo;
            if (userInfo == null)
            {
                edtUserInfo.Text = string.Empty;
                return;
            }

            var json = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(userInfo.RawJson), Formatting.Indented);
            var value = $"ProviderName : {result.ProviderName}\r\n" +
                                $"UserID : {userInfo.UserId}\r\n" +
                                $"UserName : {userInfo.UserName}\r\n" +
                                $"Email : {userInfo.Email}\r\n" +
                                $"RawJson : \r\n{json}";
            edtUserInfo.Text = value;
        }

        /// <summary>
        /// 執行登入。
        /// </summary>
        /// <param name="clientName">用戶端名稱。</param>
        private async void Login(string clientName)
        {
            var result = await OAuth2Manager.Login(clientName);
            ShowResult(result);
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            Login("Google");
        }

        private void btnFacebook_Click(object sender, EventArgs e)
        {
            Login("Facebook");
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            Login("Line");
        }

        private void btnAzure_Click(object sender, EventArgs e)
        {
            Login("Azure");
        }

        private void btnAuth0_Click(object sender, EventArgs e)
        {
            Login("Auth0");
        }
    }
}
