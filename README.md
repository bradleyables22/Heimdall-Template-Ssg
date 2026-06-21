# Heimdall SSG App Template

This repository contains the ASP.NET Core static site generation starter template for Heimdall. It demonstrates explicit route generation, shared layouts, path-base aware assets, MVC view rendering, markdown rendering, generated sitemap and robots.txt files, and hybrid pages that can still use Heimdall runtime actions when served by the ASP.NET Core app.

The template application components included in this repository are examples. Keep the pieces that fit your app and remove the rest.

Full documentation:

https://heimdall-framework.org

---

## How To Install

Install the SSG template from NuGet:

```powershell
dotnet new install HeimdallFramework.Templates.SsgApp
```

Create a new SSG app:

```powershell
dotnet new heimdall-ssg -n MyHeimdallDocs
```

Run it:

```powershell
cd MyHeimdallDocs
dotnet run
```

---

## What This Template Includes

- ASP.NET Core app configured for Heimdall static site generation
- Explicit `WithStaticPage(...)` route registration
- A shared path-base aware site layout
- FluentHtml page rendering
- MVC view rendering for generated pages
- Markdown rendering with Markdig
- Hybrid static/runtime page using a Heimdall content action
- Web root asset copying
- Static web asset copying for the Heimdall runtime
- Favicon and stylesheet assets
- Generated `404.html`
- Generated `sitemap.xml`
- Generated `robots.txt`
- Generated `heimdall.static.manifest.json`
- Build-time generation through `GenerateHeimdallStaticSiteOnBuild`

Generated sample routes:

- `/`
- `/mvc-view/`
- `/markdown/`
- `/hybrid/`
- `/404.html`
- `/sitemap.xml`
- `/robots.txt`

---

## Generate Static Output

The template generates static output after a successful build:

```powershell
dotnet build
```

You can also generate explicitly:

```powershell
dotnet run -- --heimdall-generate-static
```

The helper also accepts:

```powershell
dotnet run -- --generate-static
dotnet run -- generate-static
```

Generated output is written to:

```text
SsgApp/wwwroot/
```

The app uses:

```csharp
options.UseWebRootPath();
options.CleanOutputPath = true;
options.CopyWebRootAssets = true;
options.CopyStaticWebAssets = true;
options.UseSitemap("https://example.com");
options.UseRobotsTxt();
```

For a separate CI/CD artifact folder, switch to:

```csharp
options.UseContentRootPath("dist");
```

---

## Runtime Serving

The request pipeline serves generated default documents and still enables Heimdall endpoints for hybrid runtime sections:

```csharp
app.MapStaticAssets();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseHeimdall();
```

`UseDefaultFiles()` must run before `UseStaticFiles()` so `/` can resolve to `wwwroot/index.html`.

`UseHeimdall()` enables the hybrid page's live content action after the generated shell has loaded.

---

## Path Base

The layout and page examples route internal links and local assets through `ctx.ToSitePath(...)`. That keeps the template root-hosted by default and supports subdirectory deployment:

```csharp
options.UsePathBase("/portal");
```

With that setting, generated links such as `/hybrid/`, `/css/site.css`, `/Images/Favicon.png`, and `/_content/...` become `/portal/hybrid/`, `/portal/css/site.css`, `/portal/Images/Favicon.png`, and `/portal/_content/...`.

---

## Package Versions

The template currently targets:

- `HeimdallFramework.Server` `3.0.0`
- `HeimdallFramework.Web` `3.0.0`
- `Markdig` `1.3.2`
- `.NET` `net10.0`

---

## Building The Template Package

This repository includes a template packer project.

```powershell
dotnet build Heimdall-Template-Ssg.slnx
dotnet pack HeimdallSsgTemplatePacker\HeimdallSsgTemplatePacker.csproj -c Release -o artifacts\packages
```

The package is emitted as:

```text
artifacts/packages/HeimdallFramework.Templates.SsgApp.3.0.0.nupkg
```

You can test the local package before publishing:

```powershell
dotnet new install .\artifacts\packages\HeimdallFramework.Templates.SsgApp.3.0.0.nupkg
dotnet new heimdall-ssg -n SmokeTestSsgApp
dotnet build .\SmokeTestSsgApp\SmokeTestSsgApp.csproj
```

---

## License

MIT
