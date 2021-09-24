using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApiContext _dbContext;

        public WalletRepository(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Transaction GetTransactionByExternalId(string externalId, string walletId)
        {
            return _dbContext.Transactions.FirstOrDefault(t => t.ExternalId == externalId && t.WalletId == walletId);
        }

        public async Task<Transaction> GetTransactionById(string transactionId)
        {
            return await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
        }

        public async Task<List<Transaction>> GetTransactionsForPlayer(string playerId)
        {
            Wallet wallet = await _dbContext.Wallets.Include(w => w.Transactions).FirstOrDefaultAsync(w => w.PlayerId == playerId);

            return wallet.Transactions;
        }

        public void SaveTransaction(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
        }
    }
}
