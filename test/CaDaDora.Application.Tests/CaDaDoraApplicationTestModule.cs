using Volo.Abp.Modularity;

namespace CaDaDora;

[DependsOn(
    typeof(CaDaDoraApplicationModule),
    typeof(CaDaDoraDomainTestModule)
    )]
public class CaDaDoraApplicationTestModule : AbpModule
{

}
