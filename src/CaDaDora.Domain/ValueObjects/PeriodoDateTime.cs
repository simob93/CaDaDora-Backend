using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace CaDaDora.ValueObjects
{
    public class PeriodoDateTime : ValueObject
    {
        public DateTime DataInizio { get; private set; }

        public DateTime? DataFine { get; private set; }

        protected PeriodoDateTime()
        {
        }

        public static PeriodoDateTime Crea(DateTime dataInizio, DateTime? dataFine)
        {
            return new PeriodoDateTime
            {
                DataInizio = dataInizio,
                DataFine = dataFine
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DataInizio;
            yield return DataFine;
        }
    }
}
