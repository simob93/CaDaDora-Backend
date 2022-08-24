using System;
using Volo.Abp.Domain.Repositories;

namespace CaDaDora.Booking
{
    public interface IBookingPrenotazioneRepository : IRepository<BookingPrenotazione, Guid>
    {
    }
}
