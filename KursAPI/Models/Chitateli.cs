using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual AbonimentnieKartochki? AbonimentnieKartochki { get; set; }
        [JsonIgnore]
        public virtual ICollection<VidachaKnig> VidachaKnigs { get; set; } = new List<VidachaKnig>();
    }
}
