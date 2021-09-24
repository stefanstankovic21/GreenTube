using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity
{
    public class Player
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public Wallet Wallet { get; set; }
    }
}
