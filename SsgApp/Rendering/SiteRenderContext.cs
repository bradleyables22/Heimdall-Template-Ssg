using SsgApp.Services;

namespace SsgApp.Rendering;

public sealed record SiteRenderContext(
    SiteContent Content,
    Func<string, string> ToSitePath);
