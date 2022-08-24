using CaDaDora.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CaDaDora;

[DependsOn(
    typeof(CaDaDoraEntityFrameworkCoreTestModule)
    )]
public class CaDaDoraDomainTestModule : AbpModule
{

}
