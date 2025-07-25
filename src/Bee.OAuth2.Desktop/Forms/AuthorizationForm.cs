﻿using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Bee.OAuth2.Desktop
{
    /// <summary>
    /// OAuth2 用戶登入界面，用於取得授權碼。
    /// </summary>
    public partial class AuthorizationForm : Form
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public AuthorizationForm()
        {
            InitializeComponent();
            // 初始化 WebView2
            WebView = new WebView2();
            WebView.Dock = DockStyle.Fill;
            Controls.Add(WebView);
            WebView.CoreWebView2InitializationCompleted += WebView_Initialized;
            WebView.EnsureCoreWebView2Async();
        }

        /// <summary>
        /// 瀏覽器。
        /// </summary>
        private WebView2 WebView { get; set; }

        /// <summary>
        /// OAuth2 整合用戶端。
        /// </summary>
        public OAuth2Client? Client { get; private set; }

        /// <summary>
        /// OAuth2 驗證流程完成後的回呼網址。
        /// </summary>
        public string? RedirectUrl { get; private set; }

        /// <summary>
        /// OAuth2 授權碼。
        /// </summary>
        public string? AuthorizationCode { get; private set; }

        /// <summary>
        /// 顯示表單。
        /// </summary>
        /// <param name="client">OAuth2 整合認證的用戶端。</param>
        /// <param name="caption">標題文字。</param>
        /// <param name="width">視窗寬度 。</param>
        /// <param name="height">視窗高度。</param>
        public string ShowForm(OAuth2Client client, string caption, int width, int height)
        {
            Client = client;
            RedirectUrl = client.Provider.GetRedirectUrl();
            Text = caption;
            Width = width;
            Height = height;
            if (ShowDialog() == DialogResult.OK)
                return AuthorizationCode ?? string.Empty;
            else
                return string.Empty;
        }

        /// <summary>
        /// WebView 的 Initialized 事件處理方法。
        /// </summary>
        private void WebView_Initialized(object? sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (Client != null && e.IsSuccess)
            {
                string authorizationUrl = Client.GetAuthorizationUrl(Guid.NewGuid().ToString());
                WebView.Source = new Uri(authorizationUrl);
                // 監聽導航事件，處理 OAuth 回應
                WebView.NavigationStarting += WebView_NavigationStarting;
            }
        }

        /// <summary>
        /// WebView 的 NavigationStarting 事件處理方法。
        /// </summary>
        private void WebView_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (Client != null && !string.IsNullOrEmpty(RedirectUrl) && e.Uri.StartsWith(RedirectUrl))
            {
                var uri = new Uri(e.Uri);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                string? code = query["code"];
                string? state = query["state"];

                if (!string.IsNullOrEmpty(code))
                {
                    AuthorizationCode = code;
                }
                if (!Client.ValidateState(state))
                {
                    throw new Exception("Validate state error");
                }
                // 關閉 WebView 視窗
                DialogResult = DialogResult.OK;
            }
        }
    }
}
