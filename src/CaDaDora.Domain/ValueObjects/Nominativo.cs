using CaDaDora.Utils;
using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace CaDaDora.ValueObjects
{
    public class Nominativo : ValueObject
    {
        public string Nome { get; private set; }

        public string Cognome { get; private set; }

        protected Nominativo()
        {
        }

        public static Nominativo Crea(string nome, string cognome)
        {
            return new Nominativo
            {
                Nome = CaDaDoraUtils.IsValidString(nome, nameof(nome)),
                Cognome = CaDaDoraUtils.IsValidString(cognome, nameof(cognome)),
            };
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Nome;
            yield return Cognome;
        }
    }
}
