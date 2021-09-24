using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenTube.Models
{
    public class WalletModel
    {
        public string PlayerId { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionModel> Transactions { get; set; }

    }
}
