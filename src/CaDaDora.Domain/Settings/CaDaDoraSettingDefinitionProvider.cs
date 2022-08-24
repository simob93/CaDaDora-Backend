using Volo.Abp.Settings;

namespace CaDaDora.Settings;

public class CaDaDoraSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CaDaDoraSettings.MySetting1));
    }
}
