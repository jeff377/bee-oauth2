﻿using System;
using System.IO;
using System.Windows.Forms;
using Bee.OAuth2;
using Bee.OAuth2.WinForms;
using Newtonsoft.Json;

namespace OAuthWinForms
{
    public partial class Form1: Form
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
        }

        private OAuthConfig LoadOAuthConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Configuration file not found.", filePath);

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<OAuthConfig>(json) ?? new OAuthConfig();
        }

        private void RegisterIfExists(string name, int width, int height, TOAuthOptions options)
        {
            if (options != null)
            {
                var client = new TOAuthClient(options)
                {
                    Caption = $"{name} Login",
                    Width = width,
                    Height = height
                };
                OAuthManager.RegisterClient(name, client);
            }
        }

        /// <summary>
        /// 顯示 OAuth2 整合認證回傳結果。
        /// </summary>
        /// <param name="result">授權碼取得相關資訊的回傳結果。</param>
        private void ShowResult(TAuthorizationResult result)
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
            var result = await OAuthManager.Login(clientName);
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
    }
}
