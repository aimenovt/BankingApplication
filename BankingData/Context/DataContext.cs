using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingData.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions o) : base(o)
        {
            
        }


    }
}
