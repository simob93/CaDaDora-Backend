using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CaDaDora.Data;

/* This is used if database provider does't define
 * ICaDaDoraDbSchemaMigrator implementation.
 */
public class NullCaDaDoraDbSchemaMigrator : ICaDaDoraDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
