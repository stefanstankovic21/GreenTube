using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IPlayerRepository
    {
        Task RegisterPlayer(Player player);
        Player GetPlayerById(string playerId);
        Task<Player> GetPlayerByIdAsync(string playerId);
        Task<List<Player>> GetPlayers();
    }
}
