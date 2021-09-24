using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IPlayersService
    {
        Player GetPlayerById(string playerId);
        Task<Player> GetPlayerByIdAsync(string playerId);
        Task<List<Player>> GetPlayers();
        Task<bool> IsUsernameExists(string username);
        Task RegisterPlayer(Player player);
    }
}