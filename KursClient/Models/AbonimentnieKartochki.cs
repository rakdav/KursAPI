using System;
using System.Collections.Generic;

namespace KursClient.Models
{
    public partial class AbonimentnieKartochki
    {
        public int ChitatelId { get; set; }
        public DateOnly DataAbonimenta { get; set; }
        public DateOnly? DataVozvrataAbonimenta { get; set; }
    }
}
