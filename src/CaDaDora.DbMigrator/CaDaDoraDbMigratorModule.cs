using CaDaDora.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace CaDaDora.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CaDaDoraEntityFrameworkCoreModule),
    typeof(CaDaDoraApplicationContractsModule)
    )]
public class CaDaDoraDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
