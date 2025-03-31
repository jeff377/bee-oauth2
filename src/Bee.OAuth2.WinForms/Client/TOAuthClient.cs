using System;
using System.Threading.Tasks;
using Bee.Base;

namespace Bee.OAuth2.WinForms
{
    /// <summary>
    /// 提供 WInForms 程式進行 OAuth2 整合認證的用戶端。
    /// </summary>
    public class TOAuthClient : TBaseOAuthClient
    {
        private TStateStorage _StateStorage = null;

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        public TOAuthClient(TOAuthOptions options) : base(options)
        {
        }

        /// <summary>
        /// /OAuth2 驗證流程中的狀態儲存機制。
        /// </summary>
        public override IStateStorage StateStorage
        {
            get
            {
                if (_StateStorage == null)
                    _StateStorage = new TStateStorage();
                return _StateStorage;
            }
        }

        /// <summary>
        /// 開啟登入界面，用戶執行登入後，回傳授權碼。
        /// </summary>
        /// <param name="caption">標題文字。</param>
        /// <param name="width">視窗寬度 。</param>
        /// <param name="height">視窗高度。</param>
        public string Authorization(string caption, int width, int height)
        {
            // 開啟 OAuth2 登入界面，若無法取得授權碼，則回傳空字串
            var form = new frmAuthorization();
            string code = form.ShowForm(this, caption, width, height);
            return code;
        }

        /// <summary>
        /// 開啟登入界面，用戶執行登入後，回傳用戶資料。
        /// </summary>
        /// <param name="caption">標題文字。</param>
        /// <param name="width">視窗寬度 。</param>
        /// <param name="height">視窗高度。</param>
        public async Task<TAuthorizationResult> Login(string caption, int width, int height)
        {
            try
            {
                // 開啟登入界面，用戶執行登入後，回傳授權碼
                string code = Authorization(caption, width, height);
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

        /// <summary>
        /// 開啟登入界面，用戶執行登入後，回傳用戶資料。
        /// </summary>
        public async Task<TAuthorizationResult> Login()
        {
            string caption = this.Provider.ProviderName + " Login";
            return await this.Login(caption, 700, 600);
        }
    }
}
