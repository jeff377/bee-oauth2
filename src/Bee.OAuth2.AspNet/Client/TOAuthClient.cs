using Bee.Base;

namespace Bee.OAuth2.AspNet
{
    /// <summary>
    /// 提供 ASP.NET 程式進行 OAuth2 整合認證的用戶端。
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
        /// OAuth2 驗證流程中的狀態儲存機制。
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
    }
}
