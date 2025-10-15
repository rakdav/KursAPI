using System;
using System.Collections.Generic;

namespace KursAPI.Models
{

    public partial class Chitateli
    {
        public int ChitatelId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public virtual AbonimentnieKartochki? AbonimentnieKartochki { get; set; }
        public virtual ICollection<VidachaKnig> VidachaKnigs { get; set; } = new List<VidachaKnig>();
    }
}
