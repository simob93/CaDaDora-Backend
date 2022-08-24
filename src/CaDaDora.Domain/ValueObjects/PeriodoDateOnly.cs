using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace CaDaDora.ValueObjects
{
    public class PeriodoDateOnly : ValueObject
    {
        public DateOnly DataInizio { get; private set; }

        public DateOnly? DataFine { get; private set; }

        protected PeriodoDateOnly()
        {
        }

        public static PeriodoDateOnly Crea(DateOnly dataInizio, DateOnly? dataFine)
        {
            return new PeriodoDateOnly
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
