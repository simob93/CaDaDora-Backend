using System;
using Volo.Abp.Domain.Repositories;

namespace CaDaDora.Booking
{
    public interface IBookingPrenotazioneBotRepository : IRepository<BookingPrenotazioneBot, Guid>
    {
    }
}
