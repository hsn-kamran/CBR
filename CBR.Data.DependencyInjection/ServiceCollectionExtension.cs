using Microsoft.Extensions.DependencyInjection;

namespace CBR.Data.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCbrData(this IServiceCollection services)
        {
            return services.AddScoped<IGetValutesService, GetValutesService>();
        }
    }
}