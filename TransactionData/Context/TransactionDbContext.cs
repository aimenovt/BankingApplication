using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionData.Models;

namespace TransactionData.Context
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions o) : base(o)
        {

        }

        public DbSet<TransferLog> TransferLogs { get; set; }
    }
}
