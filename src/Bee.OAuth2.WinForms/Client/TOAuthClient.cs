﻿using System;
using System.Threading.Tasks;

namespace Bee.OAuth2.WinForms
{
    /// <summary>
    /// 提供 WInForms 程式進行 OAuth2 整合認證的用戶端。
    /// </summary>
    public class TOAuthClient : TBaseOAuthClient
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public TOAuthClient(TOAuthOptions options) : base(options)
        {
        }

        /// <summary>
        /// 標題文字。
        /// </summary>
        public string Caption { get; set; } = string.Empty;

        /// <summary>
        /// 視窗寬度。
        /// </summary>
        public int Width { get; set; } = 800;

        /// <summary>
        /// 視窗高度。
        /// </summary>
        public int Height { get; set; } = 600;

        /// <summary>
        /// /OAuth2 驗證流程中的狀態儲存機制。
        /// </summary>
        public override IStateStorage StateStorage { get; } = new TStateStorage();

        /// <summary>
        /// 開啟登入界面，用戶執行登入後，回傳授權碼。
        /// </summary>
        public string Authorization()
        {
            string caption = string.IsNullOrWhiteSpace(Caption) ? this.Provider.ProviderName + " Login" : Caption;

            // 開啟 OAuth2 登入界面，若無法取得授權碼，則回傳空字串
            var form = new frmAuthorization();
            string code = form.ShowForm(this, caption, Width, Height);
            return code;
        }

        /// <summary>
        /// 開啟登入界面，用戶執行登入後，回傳用戶資料。
        /// </summary>
        public async Task<TAuthorizationResult> Login()
        {
            try
            {
                // 開啟登入界面，用戶執行登入後，回傳授權碼
                string code = Authorization();
                return await this.ValidateAuthorization(code);
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
