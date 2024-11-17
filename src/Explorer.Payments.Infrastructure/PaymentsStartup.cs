using Explorer.Payments.Core.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
        }

        // Dependencies for repositories
        private static void SetupInfrastructure(IServiceCollection services)
        {
            
        }
    }
}
