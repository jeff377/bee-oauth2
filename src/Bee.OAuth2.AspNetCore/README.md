
# Bee.OAuth2.AspNetCore

Bee.OAuth2.AspNetCore is an ASP.NET Core library that simplifies OAuth2 authentication integration in your web applications. It supports **Google, Facebook, LINE, and Azure** via a centralized `TOAuth2Manager` with full DI support and PKCE.

## 📦 Installation

Install via NuGet Package Manager:

```sh
dotnet add package Bee.OAuth2.AspNetCore
```

## 🌍 Supported OAuth2 Providers

- ✅ Google
- ✅ Facebook
- ✅ LINE
- ✅ Azure (Microsoft Entra ID)

## 🚀 Usage Example

### Configure in `Program.cs`

```csharp
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddSingleton<TOAuth2Manager>(provider =>
{
    var http = provider.GetRequiredService<IHttpContextAccessor>();

    var options = new TOAuth2Options
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "https://localhost:5001/auth/callback",
        UsePkce = true
    };

    var client = new TOAuth2Client(options, http);
    var manager = new TOAuth2Manager(http);
    manager.RegisterClient("Google", client);
    return manager;
});
```

### Controller

```csharp
public class AuthController : Controller
{
    private readonly TOAuth2Manager _oauth2Manager;

    public AuthController(TOAuth2Manager oauth2Manager)
    {
        _oauth2Manager = oauth2Manager;
    }

    public IActionResult Login()
    {
        _oauth2Manager.RedirectToAuthorization("Google");
        return new EmptyResult();
    }

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
```

## 📜 License

This project is licensed under the MIT License.

---

# Bee.OAuth2.AspNetCore（中文）

Bee.OAuth2.AspNetCore 是一套針對 ASP.NET Core 網站設計的 OAuth2 認證整合函式庫。透過集中式的 `TOAuth2Manager` 搭配 DI 註冊，輕鬆整合 **Google、Facebook、LINE、Azure** 登入，並支援 PKCE 流程。

## 📦 安裝方式

透過 NuGet 安裝：

```sh
dotnet add package Bee.OAuth2.AspNetCore
```

## 🌍 支援的 OAuth2 提供者

- ✅ Google
- ✅ Facebook
- ✅ LINE
- ✅ Azure（Microsoft Entra ID）

## 🚀 使用範例

### 在 `Program.cs` 註冊

```csharp
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddSingleton<TOAuth2Manager>(provider =>
{
    var http = provider.GetRequiredService<IHttpContextAccessor>();

    var options = new TOAuth2Options
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "https://localhost:5001/auth/callback",
        UsePkce = true
    };

    var client = new TOAuth2Client(options, http);
    var manager = new TOAuth2Manager(http);
    manager.RegisterClient("Google", client);
    return manager;
});
```

### 控制器

```csharp
public class AuthController : Controller
{
    private readonly TOAuth2Manager _oauth2Manager;

    public AuthController(TOAuth2Manager oauth2Manager)
    {
        _oauth2Manager = oauth2Manager;
    }

    public IActionResult Login()
    {
        _oauth2Manager.RedirectToAuthorization("Google");
        return new EmptyResult();
    }

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
```

## 📜 授權

本專案採用 MIT License。
