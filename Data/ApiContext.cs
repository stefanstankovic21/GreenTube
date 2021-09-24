using Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
