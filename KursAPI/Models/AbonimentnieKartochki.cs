using System;
using System.Collections.Generic;

namespace KursAPI.Models
{

    public partial class AbonimentnieKartochki
    {
        public int ChitatelId { get; set; }
        public DateOnly DataAbonimenta { get; set; }
        public DateOnly? DataVozvrataAbonimenta { get; set; }
        public virtual Chitateli Chitatel { get; set; } = null!;
    }
}
