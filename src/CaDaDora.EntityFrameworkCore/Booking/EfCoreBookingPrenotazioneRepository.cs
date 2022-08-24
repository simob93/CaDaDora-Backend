using CaDaDora.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CaDaDora.Booking
{
    public class EfCoreBookingPrenotazioneRepository : EfCoreRepository<CaDaDoraDbContext, BookingPrenotazione, Guid>, IBookingPrenotazioneRepository
    {
        public EfCoreBookingPrenotazioneRepository(IDbContextProvider<CaDaDoraDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
