using Data;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class PlayersService : IPlayersService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Player GetPlayerById(string playerId)
        {
            return _playerRepository.GetPlayerById(playerId);
        }

        public async Task<Player> GetPlayerByIdAsync(string playerId)
        {
            return await _playerRepository.GetPlayerByIdAsync(playerId);
        }

        public async Task<List<Player>> GetPlayers()
        {
            return await _playerRepository.GetPlayers();
        }

        public async Task RegisterPlayer(Player player)
        {
            if (string.IsNullOrWhiteSpace(player.Username))
            {
                throw new ArgumentException("Username is required!");
            }
            if (await IsUsernameExists(player.Username))
            {
                throw new ArgumentException("Player with this username is already exists");
            }
            
            await _playerRepository.RegisterPlayer(player);
        }

        public async Task<bool> IsUsernameExists(string username)
        {
            var players = await GetPlayers();

            if (players.Any(p => p.Username.ToLower() == username.ToLower()))
            {
                return true;
            }

            return false;
        }
    }
}
