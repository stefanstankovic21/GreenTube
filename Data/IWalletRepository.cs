using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public interface IWalletRepository
    {
        void SaveTransaction(Transaction transaction);
        Task<List<Transaction>> GetTransactionsForPlayer(string playerId);
        Task<Transaction> GetTransactionById(string transactionId);
        Transaction GetTransactionByExternalId(string externalId, string walletId);
    }
}
