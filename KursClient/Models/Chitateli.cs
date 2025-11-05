using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KursClient.Models
{

    public partial class Chitateli
    {
        public int ChitatelId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
