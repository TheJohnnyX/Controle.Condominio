using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Promocao
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<Promover> Promover { get; set; }
        public DbSet<PromoverTransacaoEfetivar> PromoverTransacaoEfetivar { get; set; }
        public DbSet<PromoverTransacaoEfetivarAssincrona> PromoverTransacaoEfetivarAssincrona { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promover>().HasKey(p => p.Id);
            modelBuilder.Entity<PromoverTransacaoEfetivar>().HasKey(p => p.Id);
            modelBuilder.Entity<PromoverTransacaoEfetivarAssincrona>().HasKey(p => p.Id);
            


            base.OnModelCreating(modelBuilder);
        }
    }
}
