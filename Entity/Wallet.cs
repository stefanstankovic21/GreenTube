using System.Collections.Generic;

namespace Entity
{
    public class Wallet
    {
        public string Id { get; set; }
        public string PlayerId { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
