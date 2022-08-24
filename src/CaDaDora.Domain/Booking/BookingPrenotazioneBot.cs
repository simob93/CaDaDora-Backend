using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaDaDora.Booking
{
    public class BookingPrenotazioneBot : FullAuditedAggregateRoot<Guid>
    {
        public DateTime DataSchedulazione { get; private set; }

        public StatoInvio StatoInvio { get; private set; }

        protected BookingPrenotazioneBot()
        {
        }

        public BookingPrenotazioneBot(
            Guid id,
            DateTime dataSchedulazione) : base(id)
        {
        }

        public void AggiornaStato(StatoInvio stato)
        {
            StatoInvio = stato;
        }

        public void Modifica(
            DateTime dataSchedulazione)
        {
            DataSchedulazione = dataSchedulazione;
        }

    }
}
