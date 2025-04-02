using System;
using System.IO;
using System.Windows.Forms;
using Bee.OAuth2;
using Bee.OAuth2.WinForms;
using Newtonsoft.Json;

namespace OAuthWinForms
{
    public partial class Form1: Form
    {
        private OAuthConfig _config = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = @"OAuthConfig.json";
            _config = LoadOAuthConfig(filePath);
        }

        private OAuthConfig LoadOAuthConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Configuration file not found.", filePath);

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<OAuthConfig>(json) ?? new OAuthConfig();
        }

        /// <summary>
        /// 執行登入。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        private async void Login(TOAuthOptions options)
        {
            var client = new TOAuthClient(options);
            var result = await client.Login("OAuth2 登入", 800, 600);  // 開啟登入界面，用戶執行登入後，回傳用戶身份的 JSON 資料
            ShowUserInfo(result.ProviderName, result.UserInfo);
        }

        /// <summary>
        /// 顯示用戶資訊。
        /// </summary>
        private void ShowUserInfo(string providerName, TUserInfo userInfo)
        {
            if (userInfo == null)
            {
                edtUserInfo.Text = string.Empty;
                return;
            }
            var json = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(userInfo.RawJson), Formatting.Indented);
            var value = $"ProviderName : {providerName}\r\n" +
                                $"UserID : {userInfo.UserId}\r\n" +
                                $"UserName : {userInfo.UserName}\r\n" +
                                $"Email : {userInfo.Email}\r\n" +
                                $"RawJson : \r\n{json}";
            edtUserInfo.Text = value;
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            var options = _config?.GoogleOAuth;
            if (options == null) { return; }
            this.Login(options);
        }

        private void btnFacebook_Click(object sender, EventArgs e)
        {
            var options = _config?.FacebookOAuth;
            if (options == null) { return; }
            this.Login(options);
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            var options = _config?.LineOAuth;
            if (options == null) { return; }
            this.Login(options);
        }

        private void btnAzure_Click(object sender, EventArgs e)
        {
            var options = _config?.AzureOAuth;
            if (options == null) { return; }
            this.Login(options);
        }
    }
}
