# Bee.OAuth2

Bee.OAuth2 is a lightweight .NET OAuth2 authentication library that supports multiple providers, including Google, Facebook, LINE, and Azure.

## Installation

Install via NuGet Package Manager:

```sh
dotnet add package Bee.OAuth2
```

## Supported OAuth2 Providers

- Google
- Facebook
- LINE
- Azure (Microsoft Entra ID)

## Usage Examples

### Google OAuth2 Authentication Example

```csharp
using Bee.OAuth2;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var options = new GoogleOAuth2Options
        {
            ClientId = "your-client-id",
            ClientSecret = "your-client-secret",
            RedirectUri = "http://localhost",
            Scopes = { "openid", "email", "profile" }
        };

        var provider = new GoogleOAuth2Provider(options);
        string authUrl = provider.GetAuthorizationUrl("random_state_string");

        Console.WriteLine($"Open this URL in your browser: {authUrl}");

        Console.Write("Enter authorization code: ");
        string code = Console.ReadLine();

        var token = await provider.GetAccessTokenAsync(code);
        var userInfo = await provider.GetUserInfoAsync(token.AccessToken);

        Console.WriteLine($"User Info: {userInfo}");
    }
}
```

### Facebook OAuth2 Authentication Example

```csharp
var options = new FacebookOAuth2Options
{
    ClientId = "your-client-id",
    ClientSecret = "your-client-secret",
    RedirectUri = "http://localhost",
    Scopes = { "public_profile", "email" }
};

var provider = new FacebookOAuth2Provider(options);
```

### LINE OAuth2 Authentication Example

```csharp
var options = new LineOAuth2Options
{
    ClientId = "your-client-id",
    ClientSecret = "your-client-secret",
    RedirectUri = "http://localhost",
    Scopes = { "profile", "openid", "email" }
};

var provider = new LineOAuth2Provider(options);
```

### Azure OAuth2 Authentication Example

```csharp
var options = new AzureOAuth2Options
{
    ClientId = "your-client-id",
    ClientSecret = "your-client-secret",
    RedirectUri = "http://localhost",
    Scopes = { "openid", "profile", "email" }
};

var provider = new AzureOAuth2Provider(options);
```

## License

This project is licensed under the MIT License.

---

# Bee.OAuth2 (中文)

Bee.OAuth2 是一個輕量級的 .NET OAuth2 驗證函式庫，支援 Google、Facebook、LINE 和 Azure（Microsoft Entra ID）。

## 安裝

透過 NuGet 安裝：

```sh
dotnet add package Bee.OAuth2
```

## 支援的 OAuth2 驗證提供者

- Google
- Facebook
- LINE
- Azure（Microsoft Entra ID）

## 使用範例

### Google OAuth2 驗證範例

```csharp
using Bee.OAuth2;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var options = new GoogleOAuth2Options
        {
            ClientId = "your-client-id",
            ClientSecret = "your-client-secret",
            RedirectUri = "http://localhost",
            Scopes = { "openid", "email", "profile" }
        };

        var provider = new GoogleOAuth2Provider(options);
        string authUrl = provider.GetAuthorizationUrl("random_state_string");

        Console.WriteLine($"請在瀏覽器中打開此 URL: {authUrl}");

        Console.Write("請輸入授權碼: ");
        string code = Console.ReadLine();

        var token = await provider.GetAccessTokenAsync(code);
        var userInfo = await provider.GetUserInfoAsync(token.AccessToken);

        Console.WriteLine($"用戶資訊: {userInfo}");
    }
}
```

### Facebook OAuth2 驗證範例

```csharp
var options = new FacebookOAuth2Options
{
    ClientId = "your-client-id",
    ClientSecret = "your-client-secret",
    RedirectUri = "http://localhost",
    Scopes = { "public_profile", "email" }
};

var provider = new FacebookOAuth2Provider(options);
```

### LINE OAuth2 驗證範例

```csharp
var options = new LineOAuth2Options
{
    ClientId = "your-client-id",
    ClientSecret = "your-client-secret",
    RedirectUri = "http://localhost",
    Scopes = { "profile", "openid", "email" }
};

var provider = new LineOAuth2Provider(options);
```

### Azure OAuth2 驗證範例

```csharp
var options = new AzureOAuth2Options
{
    ClientId = "your-client-id",
    ClientSecret = "your-client-secret",
    RedirectUri = "http://localhost",
    Scopes = { "openid", "profile", "email" }
};

var provider = new AzureOAuth2Provider(options);
```

## 授權

本專案採用 MIT License。

