using CaDaDora.ValueObjects;
using JetBrains.Annotations;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaDaDora.Booking
{
    public class BookingPrenotazione : FullAuditedAggregateRoot<Guid>
    {
        public long PrenotazioneBookingId { get; private set; }

        public Nominativo Nominativo { get; private set; }

        public PeriodoDateOnly PeriodoPrenotato { get; private set; }

        public DateTime DataPrenotazione { get; private set; }

        public int NumeroPersone { get; private set; }

        public int? NumeroAdulti { get; private set; }

        public int? NumeroBambini { get; private set; }

        public decimal ImpostaDiSoggiorno { get; private set; }

        public decimal CostoTassaDiSoggiorno { get; private set; }

        public string EtaBambini { get; private set; }

        public decimal CostoAppartamento { get; private set; }

        public decimal? CostoCommissione { get; private set; }

        public decimal PercentualeTransazione { get; private set; }

        public decimal CostoTransazione { get; private set; }

        public int NumeroDiNotti => (PeriodoPrenotato.DataFine.Value.DayNumber - PeriodoPrenotato.DataInizio.DayNumber);

        protected BookingPrenotazione()
        {
        }

        public void Modifica(
            [NotNull] PeriodoDateOnly periodoPrenotato,
            int numeroPersone,
            int? numeroAdulti,
            int? numeroBambini,
            string etaBambini,
            decimal costoAppartamento,
            decimal? costoCommissione,
            decimal impostaDiSoggiorno)
        {
            PeriodoPrenotato = periodoPrenotato;
            NumeroPersone = numeroPersone;
            NumeroAdulti = numeroAdulti;
            NumeroBambini = numeroBambini;
            EtaBambini = etaBambini;
            CostoAppartamento = costoAppartamento;
            CostoCommissione = costoCommissione;

            if (PeriodoPrenotato.DataInizio > DateOnly.FromDateTime(DateTime.Now))
            {
                ImpostaDiSoggiorno = impostaDiSoggiorno;
                CalcolaTassaDiSoggiorno();
            }
            CalcolaCostoTransazione();
        }

        private void CalcolaCostoTransazione()
        {
            CostoTransazione = CostoAppartamento * (PercentualeTransazione / 100);
        }

        private void CalcolaTassaDiSoggiorno(decimal? impostaDiSoggiorno = null)
        {
            var paganti = NumeroPersone; //default

            if (NumeroBambini.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(EtaBambini))
                {
                    var arrayEtaBambini = EtaBambini.Trim().Split(",");
                    foreach (var etaB in arrayEtaBambini)
                    {
                        var etaBInt = int.Parse(etaB);
                        if (etaBInt <= 14)
                        {
                            paganti--;
                        }
                    }
                }
            }

            CostoTassaDiSoggiorno = NumeroDiNotti * (paganti * (impostaDiSoggiorno.HasValue ? impostaDiSoggiorno.Value : ImpostaDiSoggiorno));
        }

        public void AggiornaTassaDiSoggiorno(decimal impostaDiSoggiorno)
        {
            CalcolaTassaDiSoggiorno(impostaDiSoggiorno);
        }

        public BookingPrenotazione(
            Guid id,
            long prenotazioneBookingId,
            [NotNull] Nominativo nominativo,
            [NotNull] PeriodoDateOnly periodoPrenotato,
            DateTime dataPrenotazione,
            int numeroPersone,
            int? numeroAdulti,
            int? numeroBambini,
            string etaBambini,
            decimal costoAppartamento,
            decimal? costoCommissione,
            decimal impostaDiSoggiorno,
            decimal percentualeTransazione) : base(id)
        {
            PrenotazioneBookingId = prenotazioneBookingId;
            Nominativo = nominativo;
            PeriodoPrenotato = periodoPrenotato;
            NumeroPersone = numeroPersone;
            NumeroAdulti = numeroAdulti;
            NumeroBambini = numeroBambini;
            EtaBambini = !string.IsNullOrWhiteSpace(etaBambini) ? etaBambini.Trim() : null;
            DataPrenotazione = dataPrenotazione;
            CostoAppartamento = costoAppartamento;
            CostoCommissione = costoCommissione;
            ImpostaDiSoggiorno = impostaDiSoggiorno;
            PercentualeTransazione = percentualeTransazione;
            CalcolaTassaDiSoggiorno();
            CalcolaCostoTransazione();
        }
    }
}
