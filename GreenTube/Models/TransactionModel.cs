using Entity.Common;

namespace GreenTube.Models
{
    public class TransactionModel
    {
        public string Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
