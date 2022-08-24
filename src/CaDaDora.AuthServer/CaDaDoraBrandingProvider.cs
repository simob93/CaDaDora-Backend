using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace CaDaDora;

[Dependency(ReplaceServices = true)]
public class CaDaDoraBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CaDaDora";
}
