using CaDaDora.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CaDaDora.Personalizzazioni
{
    public class EfCorePersonalizzazioniRepository : EfCoreRepository<CaDaDoraDbContext, Personalizzazioni, Guid>, IPersonalizzazioniRepository
    {
        public EfCorePersonalizzazioniRepository(IDbContextProvider<CaDaDoraDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
