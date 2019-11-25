using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;

namespace Teste.Context
{
    public class AppDtContext : DbContext
    {
        public AppDtContext(DbContextOptions<AppDtContext> options) 
            : base(options)
        {}
        public DbSet<Usuario> Usuario { get; set; }
    }
}
