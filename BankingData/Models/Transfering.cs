using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingData.Models
{
    public class Transfering
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
