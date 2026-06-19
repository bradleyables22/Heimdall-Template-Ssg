using Heimdall.Server;
using SsgApp.Helpers;
using SsgApp.Rendering;
using SsgApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStaticWebAssets();

builder.Services.AddSingleton<SiteContent>();
builder.Services.AddSingleton<MarkdownPageRenderer>();
builder.Services.AddScoped<StaticMvcRenderer>();
builder.Services.AddAntiforgery();
builder.Services.AddHeimdall(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});
builder.Services.AddHeimdallMvc();
builder.Services
    .AddHeimdallStaticSiteGeneration(options =>
    {
        options.UseWebRootPath();
        options.CleanOutputPath = true;
        options.CopyWebRootAssets = true;
        options.CopyStaticWebAssets = true;
        options.UseSitemap("https://example.com");
        options.UseRobotsTxt();
    })
    .WithStaticPage("/", ctx => RenderAssistant.RenderPage(ctx, "Heimdall SSG Docs", "/", HomePage.Render))
    .WithStaticPage("/mvc-view", RenderAssistant.RenderMvcViewPageAsync)
    .WithStaticPage("/markdown", RenderAssistant.RenderMarkdownPageAsync)
    .WithStaticPage("/hybrid", ctx => RenderAssistant.RenderPage(ctx, "Hybrid Docs", "/hybrid/", HybridPage.Render))
    .WithNotFoundPage(ctx => RenderAssistant.RenderPage(ctx, "Not Found", "/404.html", NotFoundPage.Render));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapStaticAssets();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseHeimdall();

await app.RunWithHeimdallStaticSiteGenerationAsync(args);
