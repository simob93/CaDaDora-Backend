using System;

namespace CaDaDora.Booking
{
    public class BookingPrenotazioneDto
    {
        public long PrenotazioneBookingId { get; set; }

        public DateTime DataInizio { get; set; }

        public DateTime DataFine { get; set; }

        public DateTime DataPrenotazione { get; set; }

        public string Stato { get; set; }

        public string Nome { get; set; }

        public string Cognome { get; set; }

        public int NumeroPersone { get; set; }

        public int? NumeroAdulti { get; set; }

        public int? NumeroBambini { get; set; }

        public string EtaBambini { get; set; }

        public decimal CostoAppartamento { get; set; }

        public decimal? CostoCommissione { get; set; }
    }
}
