using Data;
using Entity;
using Entity.Common;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class DataSeedService : IDataSeedService
    {
        public async Task InitData(ApiContext context)
        {
            Player TestPlayer = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Username = "TestPlayer"
            };
            context.Players.Add(TestPlayer);

            Wallet TestWallet = new Wallet()
            {
                Id = Guid.NewGuid().ToString(),
                PlayerId = TestPlayer.Id,
                Balance = 100
            };
            context.Wallets.Add(TestWallet);

            Transaction TestTransaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                WalletId = TestWallet.Id,
                Type = TransactionType.Deposit,
                Amount = 100,
                ExternalId = "1"
            };
            context.Transactions.Add(TestTransaction);

            await context.SaveChangesAsync();
        }
    }
}
