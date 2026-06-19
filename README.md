# Heimdall SSG Documentation Template

This project is a small ASP.NET Core documentation app for testing Heimdall static site generation against the local Heimdall source tree. It demonstrates four production-shaped documentation page styles:

- `/` documents the SSG overview and is generated from fluent Heimdall HTML.
- `/mvc-view/` documents MVC rendering and is generated from a Razor partial.
- `/markdown/` documents markdown rendering and is generated from a Markdig-rendered markdown file.
- `/hybrid/` documents hybrid static/runtime sections and hydrates a live panel with a Heimdall load action when the app is running.

`SsgApp` references `Heimdall.Server` and `Heimdall.Web` as project dependencies:

```xml
<ProjectReference Include="..\..\Heimdall\Heimdall.Server\Heimdall.Server.csproj" />
<ProjectReference Include="..\..\Heimdall\Heimdall.Web\Heimdall.Web.csproj" />
```

That lets the template exercise v3 SSG and hybrid runtime changes without waiting for a NuGet package.

Because project references do not import NuGet `buildTransitive` assets, the template imports Heimdall's local MSBuild target file directly. A packaged consumer only needs the package reference and the property.

```xml
<GenerateHeimdallStaticSiteOnBuild>true</GenerateHeimdallStaticSiteOnBuild>
```

## Run The App

```bash
dotnet run --project SsgApp
```

Served pages:

- `http://localhost:5029/`
- `http://localhost:5029/mvc-view/`
- `http://localhost:5029/markdown/`
- `http://localhost:5029/hybrid/`
- `http://localhost:5029/404.html`
- `http://localhost:5029/sitemap.xml`
- `http://localhost:5029/robots.txt`

## Generate Static Output

```bash
dotnet run --project SsgApp -- --heimdall-generate-static
```

The helper also accepts `--generate-static` and `generate-static` for manual runs.

The template also generates static output after a successful build:

```bash
dotnet build
```

The template writes generated pages to the ASP.NET Core web root so the generated output matches the public static root:

```csharp
options.UseWebRootPath();
options.CleanOutputPath = true;
options.UseSitemap("https://example.com");
options.UseRobotsTxt();
```

The layout and page examples route internal links and local assets through `ctx.ToSitePath(...)`. That keeps the template root-hosted by default, while letting a subdirectory deployment switch to:

```csharp
options.UsePathBase("/portal");
```

With that setting, generated links such as `/hybrid/`, `/css/site.css`, and `/_content/...` become `/portal/hybrid/`, `/portal/css/site.css`, and `/portal/_content/...`.

The request pipeline serves those generated default documents with:

```csharp
app.MapStaticAssets();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseHeimdall();
```

`UseDefaultFiles()` must run before `UseStaticFiles()` so `/` can resolve to `wwwroot/index.html`.
`UseHeimdall()` enables the hybrid page's live content action after the generated shell has loaded.

For a clean CI/CD artifact folder instead, use:

```csharp
options.UseContentRootPath("dist");
```

Output is written to:

```text
SsgApp/wwwroot/
```

The generated output includes HTML pages, copied static web assets, `404.html`, `sitemap.xml`, `robots.txt`, and `heimdall.static.manifest.json`. The manifest lets later clean runs remove stale generated files without deleting hand-authored web root assets.
