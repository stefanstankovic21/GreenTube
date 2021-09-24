using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApiContext _dbContext;

        public PlayerRepository(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Player GetPlayerById(string playerId)
        {
            return _dbContext.Players.Include(p => p.Wallet).ThenInclude(w => w.Transactions)
                .FirstOrDefault(p => p.Id == playerId);
        }

        public async Task<Player> GetPlayerByIdAsync(string playerId)
        {
            return await _dbContext.Players.Include(p => p.Wallet).ThenInclude(w => w.Transactions)
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task<List<Player>> GetPlayers()
        {
            return await _dbContext.Players.ToListAsync();
        }

        public async Task RegisterPlayer(Player player)
        {
            player.Id = Guid.NewGuid().ToString();
            _dbContext.Players.Add(player);

            Wallet wallet = new Wallet()
            {
                Id = Guid.NewGuid().ToString(),
                PlayerId = player.Id
            };
            _dbContext.Wallets.Add(wallet);

            await _dbContext.SaveChangesAsync();
        }
    }
}
