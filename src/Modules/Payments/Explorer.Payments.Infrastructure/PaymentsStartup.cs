using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.Core.Mappers;
using Explorer.Payments.Core.UseCases.Shopping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.Infrastructure.Database.Repositories;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Explorer.Payments.API.Internal.Shopping;
using Explorer.Payments.Core.UseCases.Wallet;
using Explorer.Payments.API.Internal.Wallet;
using Explorer.Payments.API.Public.Wallet;

namespace Explorer.Payments.Infrastructure
{
    public static class PaymentsStartup
    {
        public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PaymentsProfile).Assembly); // Preduslov da imamo ovu liniju koda je da smo definisali već Profile klasu u Core/Mappers
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        // Dependencies for services
        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IShoppingService, ShoppingService>();
            services.AddScoped<ITourPurchaseTokenService, TourPurchaseTokenService>();
            services.AddScoped<IWalletInternalService, WalletService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ISaleService, SaleService>();
        }

        // Dependencies for repositories
        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, PaymentsContext>));
            services.AddScoped<ITourPurchaseTokenRepository, TourPurchaseTokenRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));
        }
    }
}
