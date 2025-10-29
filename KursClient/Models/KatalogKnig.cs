using System;
using System.Collections.Generic;

namespace KursClient.Models
{

    public partial class KatalogKnig
    {
        public int IdKniga { get; set; }
        public string Author { get; set; } = null!;
        public int Year { get; set; }
        public string Publisher { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int Kolichestvo { get; set; }

    }
}
