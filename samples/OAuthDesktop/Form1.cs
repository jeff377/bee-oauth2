using Bee.OAuth2;
using Bee.OAuth2.Desktop;
using Newtonsoft.Json;

namespace OAuthDesktop
{
    public partial class Form1 : Form
    {
        private OAuthConfig? _config = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath =  "OAuthConfig.json";
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
        /// ����n�J�C
        /// </summary>
        /// <param name="options">OAuth2 �]�w�ﶵ�C</param>
        private async void Login(TOAuthOptions options)
        {
            var client = new TOAuthClient(options);
            var result = await client.Login("OAuth2 �n�J", 800, 600);  // �}�ҵn�J�ɭ��A�Τ����n�J��A�^�ǥΤᨭ���� JSON ���
            ShowUserInfo(result.ProviderName, result.UserInfo);
        }

        /// <summary>
        /// ��ܥΤ��T�C
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


    }
}
