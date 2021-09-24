using Data;
using Entity;
using Entity.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class WalletsService : IWalletsService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IPlayersService _playersService;
        private readonly ConcurrentDictionary<string, object> transactions = new ConcurrentDictionary<string, object>();

        public WalletsService(IWalletRepository walletRepository, IPlayersService playersService)
        {
            _walletRepository = walletRepository;
            _playersService = playersService;
        }

        public async Task<Transaction> GetTransactionById(string transactionId)
        {
            return await _walletRepository.GetTransactionById(transactionId);
        }

        public async Task<List<Transaction>> GetTransactionsForPlayer(string playerId)
        {
            return await _walletRepository.GetTransactionsForPlayer(playerId);
        }

        public void MakeTransaction(string playerId, Transaction transaction)
        {
            lock (transactions.GetOrAdd(playerId, new object()))
            {
                Player player = _playersService.GetPlayerById(playerId);
                if (player == null)
                {
                    throw new ArgumentNullException("Player with this id does not exists");
                }

                var existingTransaction = _walletRepository.GetTransactionByExternalId(transaction.ExternalId, player.Wallet.Id);
                if (existingTransaction != null)
                {
                    return;
                }

                transaction.Id = Guid.NewGuid().ToString();
                transaction.WalletId = player.Wallet.Id;

                switch (transaction.Type)
                {
                    case TransactionType.Win:
                    case TransactionType.Deposit:
                        player.Wallet.Balance += transaction.Amount;
                        break;
                    case TransactionType.Stake:
                        if (IsTransactionValid(player.Wallet.Balance, transaction))
                        {
                            player.Wallet.Balance -= transaction.Amount;
                            break;
                        }
                        else
                        {
                            throw new ArgumentException("Not possible to relize transaction with this amount!");
                        }
                    default:
                        break;
                }

                _walletRepository.SaveTransaction(transaction);
            }
        }

        private bool IsTransactionValid(decimal playerBalance, Transaction transaction)
        {
            var isValid = true;
            if (transaction.Type == TransactionType.Stake)
            {
                if (playerBalance < transaction.Amount)
                {
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}
