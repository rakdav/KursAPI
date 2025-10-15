using System;
using System.Collections.Generic;

namespace KursAPI.Models
{

    public partial class VidachaKnig
    {
        public int Id { get; set; }

        public int ChitatelId { get; set; }

        public DateOnly DataVidachi { get; set; }

        public DateOnly DataVozvrata { get; set; }

        public int IdKniga { get; set; }

        public virtual Chitateli Chitatel { get; set; } = null!;

        public virtual KatalogKnig IdKnigaNavigation { get; set; } = null!;
    }
}
