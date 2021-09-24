using Data;
using System.Threading.Tasks;

namespace Services
{
    public interface IDataSeedService
    {
        Task InitData(ApiContext context);
    }
}