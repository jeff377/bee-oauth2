# Bee.OAuth2.AspNet

Bee.OAuth2.AspNet is an OAuth2 authentication library designed for **ASP.NET WebForms / MVC**, supporting **Google, Facebook, LINE, and Azure**.

## 📦 Installation

Install via NuGet Package Manager:

```sh
dotnet add package Bee.OAuth2.AspNet
```

## 🌍 Supported OAuth2 Providers

- ✅ Google
- ✅ Facebook
- ✅ LINE
- ✅ Azure (Microsoft Entra ID)

## 🚀 Usage Example (ASP.NET WebForms)

### Register Google OAuth2 in Global.asax

```csharp
using Bee.OAuth2;
using Bee.OAuth2.AspNet;

protected void Application_Start()
{
    var options = new TGoogleOAuth2Options()
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "your-redirect-uri",
        UsePKCE = true
    };
    var client = new TOAuth2Client(options);
    OAuth2Manager.RegisterClient("Google", client);
}
```

### Redirect to OAuth Authorization Page in Login Page

```csharp
OAuth2Manager.RedirectToAuthorization("Google");
```

### Validate OAuth2 Callback and Retrieve User Information

```csharp
var result = await OAuth2Manager.ValidateAuthorization();
Response.Write(
    $"ProviderName : {result.ProviderName}<br/>" +
    $"UserID : {result.UserInfo.UserId}<br/>" +
    $"UserName : {result.UserInfo.UserName}<br/>" +
    $"Email : {result.UserInfo.Email}<br/>" +
    $"RawJson : {result.UserInfo.RawJson}");
```

## 🚀 Usage Example (ASP.NET MVC)

### Register Google OAuth2 in Startup.cs

```csharp
using Bee.OAuth2;
using Bee.OAuth2.AspNet;

public void Configuration(IAppBuilder app)
{
    var options = new TGoogleOAuth2Options()
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "your-redirect-uri",
        UsePKCE = true
    };
    var client = new TOAuth2Client(options);
    OAuth2Manager.RegisterClient("Google", client);
}
```

### Redirect to OAuth2 Authorization Page in Controller

```csharp
public ActionResult Login()
{
    return Redirect(OAuth2Manager.GetAuthorizationUrl("Google"));
}
```

### Validate OAuth2 Callback and Retrieve User Information in Controller

```csharp
public async Task<ActionResult> Callback()
{
    var result = await OAuth2Manager.ValidateAuthorization();
    return Content($"ProviderName: {result.ProviderName}\n" +
                   $"UserID: {result.UserInfo.UserId}\n" +
                   $"UserName: {result.UserInfo.UserName}\n" +
                   $"Email: {result.UserInfo.Email}\n" +
                   $"RawJson: {result.UserInfo.RawJson}");
}
```

## 🔐 Key Setup

### Generate a secure key for `state` encryption

Bee.OAuth2.AspNet uses `AES-CBC + HMAC` to protect the OAuth2 `state` parameter. You must generate a 64-byte combined key and store it in the environment variable `OAUTH2_STATE_KEY`.

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

# Bee.OAuth2.AspNet（中文）

Bee.OAuth2.AspNet 是一個專為 **ASP.NET WebForms / MVC** 設計的 OAuth2 認證函式庫，支援 **Google、Facebook、LINE、Azure** 等 OAuth2 提供者。

## 📦 安裝方式

透過 NuGet 安裝：

```sh
dotnet add package Bee.OAuth2.AspNet
```

## 🌍 支援的 OAuth2 提供者

- ✅ Google
- ✅ Facebook
- ✅ LINE
- ✅ Azure（Microsoft Entra ID）

## 🚀 使用範例（ASP.NET WebForms）

### 在 Global.asax 註冊 Google OAuth2

```csharp
using Bee.OAuth2;
using Bee.OAuth2.Providers;

protected void Application_Start()
{
    var options = new TGoogleOAuth2Options()
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "your-redirect-uri",
        UsePKCE = true
    };
    var client = new TOAuth2Client(options);
    OAuth2Manager.RegisterClient("Google", client);
}
```

### 在 login 頁面轉向 OAuth2 授權頁面

```csharp
OAuth2Manager.RedirectToAuthorization("Google");
```

### 在 callback 頁面驗證 OAuth2 回傳授權碼，並取得用戶資料

```csharp
var result = await OAuth2Manager.ValidateAuthorization();
Response.Write(
    $"ProviderName : {result.ProviderName}<br/>" +
    $"UserID : {result.UserInfo.UserId}<br/>" +
    $"UserName : {result.UserInfo.UserName}<br/>" +
    $"Email : {result.UserInfo.Email}<br/>" +
    $"RawJson : {result.UserInfo.RawJson}");
```

## 🚀 使用範例（ASP.NET MVC）

### 在 Startup.cs 註冊 Google OAuth2

```csharp
using Bee.OAuth2;
using Bee.OAuth2.Providers;

public void Configuration(IAppBuilder app)
{
    var options = new TGoogleOAuth2Options()
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "your-redirect-uri",
        UsePKCE = true
    };
    var client = new TOAuth2Client(options);
    OAuth2Manager.RegisterClient("Google", client);
}
```

### 在 Controller 中轉向 OAuth 授權頁面

```csharp
public ActionResult Login()
{
    return Redirect(OAuth2Manager.GetAuthorizationUrl("Google"));
}
```

### 在 Controller 中驗證 OAuth2 回傳授權碼，並取得用戶資料

```csharp
public async Task<ActionResult> Callback()
{
    var result = await OAuth2Manager.ValidateAuthorization();
    return Content($"ProviderName: {result.ProviderName}\n" +
                   $"UserID: {result.UserInfo.UserId}\n" +
                   $"UserName: {result.UserInfo.UserName}\n" +
                   $"Email: {result.UserInfo.Email}\n" +
                   $"RawJson: {result.UserInfo.RawJson}");
}
```

## 🔐 金鑰設定

### 產生用於加密 `state` 的安全金鑰

Bee.OAuth2.AspNet 使用 `AES-CBC + HMAC` 演算法保護 OAuth2 的 `state` 參數。你必須先產生一組 64 位元組的組合金鑰，並設定為 `OAUTH2_STATE_KEY` 環境變數。

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

