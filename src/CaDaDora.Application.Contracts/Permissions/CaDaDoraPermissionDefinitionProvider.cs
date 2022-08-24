using CaDaDora.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CaDaDora.Permissions;

public class CaDaDoraPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CaDaDoraPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CaDaDoraPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CaDaDoraResource>(name);
    }
}
