using Bee.OAuth2.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace OAuthAspNetCore.Controllers
{
    public class AuthController : Controller
    {
        private readonly OAuth2Manager _oauth2Manager;

        public AuthController(OAuth2Manager oauth2Manager)
        {
            _oauth2Manager = oauth2Manager;
        }

        [HttpGet("/auth/login")]
        public IActionResult Login()
        {
            _oauth2Manager.RedirectToAuthorization("Google");
            return new EmptyResult();
        }

        [HttpGet("/auth/callback")]
        public async Task<IActionResult> Callback()
        {
            var result = await _oauth2Manager.ValidateAuthorization();
            if (result.IsSuccess)
            {
                return Content($"ProviderName: {result.ProviderName}\n" +
                               $"UserID: {result.UserInfo.UserId}\n" +
                               $"UserName: {result.UserInfo.UserName}\n" +
                               $"Email: {result.UserInfo.Email}\n" +
                               $"RawJson: {result.UserInfo.RawJson}");
            }
            else
            {
                return Content($"Error: {result.Exception?.Message}");
            }
        }
    }
}
