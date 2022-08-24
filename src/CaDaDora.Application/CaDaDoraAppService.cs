using System;
using System.Collections.Generic;
using System.Text;
using CaDaDora.Localization;
using Volo.Abp.Application.Services;

namespace CaDaDora;

/* Inherit your application services from this class.
 */
public abstract class CaDaDoraAppService : ApplicationService
{
    protected CaDaDoraAppService()
    {
        LocalizationResource = typeof(CaDaDoraResource);
    }
}
