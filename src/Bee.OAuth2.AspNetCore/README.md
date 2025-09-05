
# Bee.OAuth2.AspNetCore

Bee.OAuth2.AspNetCore is an ASP.NET Core library that simplifies OAuth2 authentication integration in your web applications. It supports **Google, Facebook, LINE, Azure, and Auth0** via a centralized `TOAuth2Manager` with full DI support and PKCE.

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
- ✅ Auth0

## 🚀 Usage Example

### Configure in `Program.cs`

```csharp
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddSingleton<OAuth2Manager>(provider =>
{
    var http = provider.GetRequiredService<IHttpContextAccessor>();

    var options = new GoogleOAuth2Options
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "https://localhost:5001/auth/callback",
        UsePkce = true
    };

    var client = new OAuth2Client(options, http);
    var manager = new OAuth2Manager(http);
    manager.RegisterClient("Google", client);
    return manager;
});
```

### Controller

```csharp
public class AuthController : Controller
{
    private readonly OAuth2Manager _oauth2Manager;

    public AuthController(OAuth2Manager oauth2Manager)
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

## 🔐 Key Setup

### Generate a secure key for `state` encryption

Bee.OAuth2.AspNet uses `AES-CBC + HMAC` to protect the OAuth2 `state` parameter. You must generate a 64-byte combined key and store it in the environment variable `OAUTH2_STATE_KEY`.

> **Note:**  
> If the environment variable `OAUTH2_STATE_KEY` is **not set**, the state value will **not be encrypted**.  
> Instead, the client name will be encoded using Base64 only.  
> This provides basic obfuscation but does **not** guarantee confidentiality or integrity.

#### 🔧 How to generate the key

```csharp
// Use this once to generate a base64 key
var key = Bee.Base.AesCbcHmacKeyGenerator.GenerateCombinedKey();
Console.WriteLine(Convert.ToBase64String(key));
```

#### ⚙️ Set the environment variable

On Windows:

1. Open **System Properties** → **Environment Variables**
2. Add a new **User** or **System** variable:

| Variable name        | Value (example)                          |
|----------------------|------------------------------------------|
| `OAUTH2_STATE_KEY`   | `VGhpcy1pcy1hLXRlc3Qta2V5LXdpdGgtNjQ...` |

3. Restart **Visual Studio** and the application.

Alternatively, you can set it using PowerShell:

```powershell
[System.Environment]::SetEnvironmentVariable("OAUTH2_STATE_KEY", "your-base64-key", "User")
```

## 📜 License

This project is licensed under the MIT License.

---

# Bee.OAuth2.AspNetCore（中文）

Bee.OAuth2.AspNetCore 是一套針對 ASP.NET Core 網站設計的 OAuth2 認證整合函式庫。透過集中式的 `TOAuth2Manager` 搭配 DI 註冊，輕鬆整合 **Google、Facebook、LINE、Azure、Auth0** 登入，並支援 PKCE 流程。

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
- ✅ Auth0

## 🚀 使用範例

### 在 `Program.cs` 註冊

```csharp
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddSingleton<OAuth2Manager>(provider =>
{
    var http = provider.GetRequiredService<IHttpContextAccessor>();

    var options = new GoogleOAuth2Options
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "https://localhost:5001/auth/callback",
        UsePkce = true
    };

    var client = new OAuth2Client(options, http);
    var manager = new OAuth2Manager(http);
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

## 🔐 金鑰設定

### 產生用於加密 `state` 的安全金鑰

Bee.OAuth2.AspNet 使用 `AES-CBC + HMAC` 演算法保護 OAuth2 的 `state` 參數。你必須先產生一組 64 位元組的組合金鑰，並設定為 `OAUTH2_STATE_KEY` 環境變數。

> **注意：**  
> 如果未設定 `OAUTH2_STATE_KEY` 環境變數，state 值將**不會加密**，  
> 而是僅以 Base64 編碼 client name。  
> 這僅提供基本遮蔽，**不保證機密性或完整性**。

#### 🔧 如何產生金鑰

```csharp
// 執行一次即可產生 Base64 格式的金鑰
var key = Bee.Base.AesCbcHmacKeyGenerator.GenerateCombinedKey();
Console.WriteLine(Convert.ToBase64String(key));
```

#### ⚙️ 設定環境變數（Windows）

1. 開啟「系統內容」 →「環境變數」
2. 在「使用者變數」或「系統變數」中新增一筆：

| 變數名稱           | 值（範例）                                 |
|--------------------|--------------------------------------------|
| `OAUTH2_STATE_KEY` | `VGhpcy1pcy1hLXRlc3Qta2V5LXdpdGgtNjQ...`    |

3. 重啟 Visual Studio 與網站應用程式。

也可以使用 PowerShell 設定：

```powershell
[System.Environment]::SetEnvironmentVariable("OAUTH2_STATE_KEY", "你的 base64 金鑰", "User")
```

## 📜 授權

本專案採用 MIT License。
