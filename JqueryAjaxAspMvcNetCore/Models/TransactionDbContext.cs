using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace JqueryAjaxAspMvcNetCore.Models
{
    public class TransactionDbContext:DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options):base(options)
        {             }

        public DbSet<TransactionModel> Transactions { get; set; }
    }
}
