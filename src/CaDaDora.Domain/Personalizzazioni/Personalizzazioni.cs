using System;
using Volo.Abp.Domain.Entities;

namespace CaDaDora.Personalizzazioni
{
    public class Personalizzazioni : Entity<Guid>
    {
        public decimal ImpostaDiSoggiorno { get; private set; }

        public decimal CostoTransazioneBancaria { get; private set; }

        protected Personalizzazioni()
        {
        }
    }
}
