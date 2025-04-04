# Bee.OAuth2.WinForms

Bee.OAuth2.WinForms is a Windows Forms library that provides a user interface for OAuth2 authentication. It supports **Google, Facebook, LINE, and Azure** authentication with an easy-to-use UI component.

## 📦 Installation

Install via NuGet Package Manager:

```sh
dotnet add package Bee.OAuth2.WinForms
```

## 🌍 Supported OAuth2 Providers

- ✅ Google
- ✅ Facebook
- ✅ LINE
- ✅ Azure (Microsoft Entra ID)

## 🚀 Usage Example

### Google OAuth2 Authentication

```csharp
using Bee.OAuth2;
using Bee.OAuth2.WinForms;

private async void GoogleOAuth2()
{
     var options = new TGoogleOAuthOptions()
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "http://localhost:5000/callback",
        UsePKCE = true
    };
    var client = new TOAuthClient(options)
    {
        Caption = "Google Login",
        Width = 600,
        Height = 800
    };
    // Open the login interface. After the user logs in, return user information.
    var result = await client.Login();
    var userinfo = $"UserID : {result.UserInfo.UserId}\r\n" +
            $"UserName : {result.UserInfo.UserName}\r\n" +
            $"Email : {result.UserInfo.Email}\r\n" +
            $"RawJson : \r\n{result.UserInfo.RawJson}";
}
```

## 📜 License

This project is licensed under the MIT License.

---

# Bee.OAuth2.WinForms（中文）

Bee.OAuth2.WinForms 是一個 Windows Forms 函式庫，提供 OAuth2 驗證的使用者介面。支援 **Google、Facebook、LINE 和 Azure** 的身份驗證，並內建易於使用的 UI 元件。

## 📦 安裝方式

透過 NuGet 安裝：

```sh
dotnet add package Bee.OAuth2.WinForms
```

## 🌍 支援的 OAuth2 提供者

- ✅ Google
- ✅ Facebook
- ✅ LINE
- ✅ Azure（Microsoft Entra ID）

## 🚀 使用範例

### Google OAuth2 驗證

```csharp
using Bee.OAuth2;
using Bee.OAuth2.WinForms;

private async void GoogleOAuth2()
{
     var options = new TGoogleOAuthOptions()
    {
        ClientId = "your-client-id",
        ClientSecret = "your-client-secret",
        RedirectUri = "http://localhost:5000/callback",
        UsePKCE = true
    };
    var client = new TOAuthClient(options)
    {
        Caption = "Google Login",
        Width = 600,
        Height = 800
    };
    // 開啟登入界面，用戶執行登入後，回傳用戶資料
    var result = await client.Login();
    var userinfo = $"UserID : {result.UserInfo.UserId}\r\n" +
            $"UserName : {result.UserInfo.UserName}\r\n" +
            $"Email : {result.UserInfo.Email}\r\n" +
            $"RawJson : \r\n{result.UserInfo.RawJson}";
}
```

## 📜 授權

本專案採用 MIT License。
