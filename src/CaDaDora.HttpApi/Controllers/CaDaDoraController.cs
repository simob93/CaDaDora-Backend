using CaDaDora.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CaDaDora.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CaDaDoraController : AbpControllerBase
{
    protected CaDaDoraController()
    {
        LocalizationResource = typeof(CaDaDoraResource);
    }
}
