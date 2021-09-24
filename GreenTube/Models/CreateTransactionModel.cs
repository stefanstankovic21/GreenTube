using Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenTube.Models
{
    public class CreateTransactionModel
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string ExternalId { get; set; }
    }
}
