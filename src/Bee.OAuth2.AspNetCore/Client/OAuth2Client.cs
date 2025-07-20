using Microsoft.AspNetCore.Http;

namespace Bee.OAuth2.AspNetCore
{
    /// <summary>
    /// 提供 ASP.NET Core 程式進行 OAuth2 整合認證的用戶端。
    /// </summary>
    public class OAuth2Client : BaseOAuth2Client
    {
        private StateStorage? _stateStorage = null;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="options">OAuth2 設定選項。</param>
        /// <param name="httpContextAccessor">提供目前 HttpContext 的存取權。</param>
        public OAuth2Client(OAuth2Options options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// OAuth2 驗證流程中的狀態儲存機制。
        /// </summary>
        public override IStateStorage StateStorage
        {
            get
            {
                return _stateStorage ??= new StateStorage(_httpContextAccessor);
            }
        }
    }
}
