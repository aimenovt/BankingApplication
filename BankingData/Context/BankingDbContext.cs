﻿using BankingData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingData.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
