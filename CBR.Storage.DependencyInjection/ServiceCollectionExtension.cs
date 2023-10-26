using CBR.Storage.UseCases.UploadCurrencies;
using CBR.Storage.UseCases.UploadCurrencyCourses;
using CBR.Storage.UseCases.UploadValutes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CBR.Storage.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCbrStorage(this IServiceCollection services, string connectionString)
    {
        return services
            .AddDbContext<CbrDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging();
                options.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
            })

            .AddScoped<ICurrencyStorage, CurrencyStorage>()
            .AddScoped<ICurrencyCourseStorage, CurrencyCourseMonthStorage>();
    }
}