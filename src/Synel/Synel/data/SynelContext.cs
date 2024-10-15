using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Synel.Models;

namespace Synel.Data
{
    public class SynelContext : DbContext
    {
        public SynelContext()
        {
            
        }
        public SynelContext (DbContextOptions<SynelContext> options)
            : base(options)
        {
        }

        public DbSet<Synel.Models.Employees> Employees { get; set; } = default!;
    }
}
