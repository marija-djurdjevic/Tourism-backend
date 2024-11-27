using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.Wallets
{
    public class Wallet : Entity
    {
        public int TouristId { get; private set; }
        public double Balance { get; private set; }

        
        public Wallet() { }

        
        public Wallet(int touristId)
        {
            TouristId = touristId;
            Balance = 0; // Starts with 0 AC
        }

        // Updates to methods like Credit/Debit can be added later
    }
}
