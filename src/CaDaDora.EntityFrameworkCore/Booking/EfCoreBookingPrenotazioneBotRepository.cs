using CaDaDora.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CaDaDora.Booking
{
    public class EfCoreBookingPrenotazioneBotRepository : EfCoreRepository<CaDaDoraDbContext, BookingPrenotazioneBot, Guid>, IBookingPrenotazioneBotRepository
    {
        public EfCoreBookingPrenotazioneBotRepository(IDbContextProvider<CaDaDoraDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
