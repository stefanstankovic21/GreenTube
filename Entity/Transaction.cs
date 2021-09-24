using Entity.Common;

namespace Entity
{
    public class Transaction
    {
        public string Id { get; set; }
        public string WalletId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string ExternalId { get; set; }
    }
}
