using BiddingApp.Infrastructure.MapperConfigs;
using Microsoft.Extensions.DependencyInjection;

namespace BiddingApp.Infrastructure.Extentions
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfig));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}