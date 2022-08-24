using System.Threading.Tasks;

namespace CaDaDora.Data;

public interface ICaDaDoraDbSchemaMigrator
{
    Task MigrateAsync();
}
