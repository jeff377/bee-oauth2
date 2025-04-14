using Bee.OAuth2.AspNetCore;
using OAuthAspNetCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// 讀取 OAuth2 設定
builder.Configuration.AddJsonFile("OAuthConfig.json", optional: false, reloadOnChange: true);

// 建立 TOAuth2Manager 並註冊各 Provider
builder.Services.AddSingleton<TOAuth2Manager>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var accessor = provider.GetRequiredService<IHttpContextAccessor>();

    var oauthConfig = config.Get<OAuthConfig>();
    var manager = new TOAuth2Manager(accessor);

    if (oauthConfig == null)
    {
        throw new InvalidOperationException("OAuth configuration is missing.");
    }
    if (oauthConfig.GoogleOAuth != null)
    {
        manager.RegisterClient("Google", new TOAuth2Client(oauthConfig.GoogleOAuth, accessor));
    }
    if (oauthConfig.FacebookOAuth != null)
    {
        manager.RegisterClient("Facebook", new TOAuth2Client(oauthConfig.FacebookOAuth, accessor));
    }
    if (oauthConfig.LineOAuth != null)
    {
        manager.RegisterClient("Line", new TOAuth2Client(oauthConfig.LineOAuth, accessor));
    }
    if (oauthConfig.AzureOAuth != null)
    {
        manager.RegisterClient("Azure", new TOAuth2Client(oauthConfig.AzureOAuth, accessor));
    }
    return manager;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
