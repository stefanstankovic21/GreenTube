using AutoMapper;
using Entity;
using GreenTube.Models;

namespace GreenTube.Mappers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Player, PlayerModel>().ReverseMap();
            CreateMap<Player, CreatePlayerModel>().ReverseMap();
            CreateMap<Transaction, TransactionModel>().ReverseMap();
            CreateMap<Transaction, CreateTransactionModel>().ReverseMap();
        }
    }
}
