using Explorer.Payments.Core.Domain.Shopping;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Payments.Core.Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database
{
    public class PaymentsContext : DbContext
    {
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Sale> Sales {  get; set; }

        public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payments");

            modelBuilder.Entity<ShoppingCart>().Property(item => item.Items).HasColumnType("jsonb");
        }
    }
}
