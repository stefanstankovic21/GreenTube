using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IWalletsService
    {
        void MakeTransaction(string playerId, Transaction transaction);
        Task<List<Transaction>> GetTransactionsForPlayer(string playerId);
        Task<Transaction> GetTransactionById(string transactionId);
    }
}
