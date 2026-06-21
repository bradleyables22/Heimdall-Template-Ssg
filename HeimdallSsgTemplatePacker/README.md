# Heimdall SSG App Template

ASP.NET Core static site generation starter for Heimdall.

Use this template when you want an app that can generate public static HTML at build time while still using the ASP.NET Core host, dependency injection, FluentHtml pages, MVC view rendering, markdown content, copied assets, sitemap generation, robots.txt, and optional live Heimdall runtime sections when the app is served dynamically.

## Install

```powershell
dotnet new install HeimdallFramework.Templates.SsgApp
dotnet new heimdall-ssg -n MyHeimdallDocs
cd MyHeimdallDocs
dotnet run
```

## Generate Static Output

The template generates static output during build:

```powershell
dotnet build
```

You can also run generation directly:

```powershell
dotnet run -- --heimdall-generate-static
```

The helper also accepts `--generate-static` and `generate-static`.

## What You Get

- Explicit Heimdall static page registration
- FluentHtml static pages
- MVC view static rendering
- Markdown rendering through Markdig
- A hybrid static page that can load live content from a Heimdall action
- Shared path-base aware layout
- Copied web root assets
- Copied Heimdall runtime static web assets
- Favicon, stylesheet, sitemap.xml, robots.txt, 404.html, and generation manifest
- `GenerateHeimdallStaticSiteOnBuild` enabled by default

## Package Versions

The template currently targets:

- `HeimdallFramework.Server` `3.0.0`
- `HeimdallFramework.Web` `3.0.0`
- `Markdig` `1.3.2`
- `.NET` `net10.0`

## Documentation

Full documentation:

https://heimdall-framework.org

## License

MIT
