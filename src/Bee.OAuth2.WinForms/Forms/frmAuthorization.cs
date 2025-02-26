using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Bee.OAuth2.WinForms
{
    /// <summary>
    /// OAuth2 用戶登入界面，用於取得授權碼。
    /// </summary>
    public partial class frmAuthorization : Form
    {
        private WebView2 _WebView = null;
        private TOAuthClient _Client = null;
        private string _RedirectUri = string.Empty;
        private string _AuthorizationCode = string.Empty;

        /// <summary>
        /// 建構函式。
        /// </summary>
        public frmAuthorization()
        {
            InitializeComponent();
            // 初始化 WebView2
            _WebView = new WebView2();
            _WebView.Dock = DockStyle.Fill;
            this.Controls.Add(_WebView);
            _WebView.CoreWebView2InitializationCompleted += WebView_Initialized;
            _WebView.EnsureCoreWebView2Async();
        }

        /// <summary>
        /// 瀏覽器。
        /// </summary>
        public WebView2 WebView
        {
            get { return _WebView; }
        }

        /// <summary>
        /// OAuth2 授權碼。
        /// </summary>
        public string AuthorizationCode
        {
            get { return _AuthorizationCode; }
        }

        /// <summary>
        /// OAuth2 驗證流程完成後的回呼網址。
        /// </summary>
        public string RedirectUri
        {
            get { return _RedirectUri; }
        }

        /// <summary>
        /// OAuth2 整合用戶端。
        /// </summary>
        public TOAuthClient Client
        {
            get { return _Client; }
        }

        /// <summary>
        /// 顯示表單。
        /// </summary>
        /// <param name="client">OAuth2 整合認證的用戶端。</param>
        /// <param name="caption">標題文字。</param>
        /// <param name="width">視窗寬度 。</param>
        /// <param name="height">視窗高度。</param>
        public string ShowForm(TOAuthClient client, string caption, int width, int height)
        {
            _Client = client;
            _RedirectUri = client.Provider.GetRedirectUri();
            this.Text = caption;
            this.Width = width;
            this.Height = height;
            if (this.ShowDialog() == DialogResult.OK)
                return this.AuthorizationCode;
            else
                return string.Empty;
        }

        /// <summary>
        /// WebView 的 Initialized 事件處理方法。
        /// </summary>
        private void WebView_Initialized(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            string sAuthorizationUrl;

            if (e.IsSuccess)
            {
                sAuthorizationUrl = this.Client.GetAuthorizationUrl(Guid.NewGuid().ToString());
                this.WebView.Source = new Uri(sAuthorizationUrl);
                // 監聽導航事件，處理 OAuth 回應
                this.WebView.NavigationStarting += WebView_NavigationStarting;
            }
        }

        /// <summary>
        /// WebView 的 NavigationStarting 事件處理方法。
        /// </summary>
        private void WebView_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (e.Uri.StartsWith(_RedirectUri))
            {
                var uri = new Uri(e.Uri);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                string code = query["code"];
                string state = query["state"];

                if (!string.IsNullOrEmpty(code))
                {
                    _AuthorizationCode = code;
                }
                if (!this.Client.ValidateState(state))
                {
                    throw new Exception("Validate state error");
                }
                // 關閉 WebView 視窗
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
