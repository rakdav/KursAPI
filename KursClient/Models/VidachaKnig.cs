
namespace KursClient.Models
{
    public partial class VidachaKnig
    {
        public int Id { get; set; }

        public int ChitatelId { get; set; }

        public DateOnly DataVidachi { get; set; }

        public DateOnly DataVozvrata { get; set; }

        public int IdKniga { get; set; }
    }
}
