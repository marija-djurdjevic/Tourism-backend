using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.WalletDtos
{
    public class WalletDto
    {
        public long Id { get; set; }
        public int TouristId { get; set; }
        public double Balance { get; set; }
        
        public WalletDto(int touristId) { 
            TouristId = touristId;
            Balance = 0;
        }
    
    }
}
