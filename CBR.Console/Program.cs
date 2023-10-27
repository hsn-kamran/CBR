using CBR.Data;
using CBR.Data.DependencyInjection;
using CBR.Storage.DependencyInjection;
using CBR.Storage.UseCases.UploadCurrencyCourses;
using CBR.Storage.UseCases.UploadValutes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// https://dbdiagram.io/d/CBR-diagram-653ab4b6ffbf5169f08a0210
// диаграмма таблиц бд

// для решения конфликта поля DateTime с колонкой типа timestamp with timezone в Postgres
// https://ru.stackoverflow.com/questions/1416392/Ошибка-cannot-write-datetime-with-kind-local-to-postgresql-type-timestamp-with
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true)
    .Build();

string connectionString = configuration.GetSection("ConnectionStrings:CBR_Postgres_Connection").Value 
    ?? throw new ArgumentNullException(nameof(configuration));

var services = new ServiceCollection();
services.AddCbrStorage(connectionString);
services.AddCbrData();

using var serviceProvider = services.BuildServiceProvider();
var getValutesService = serviceProvider.GetRequiredService<IGetValutesService>();

var currencyStorage = serviceProvider.GetRequiredService<ICurrencyStorage>();
var currencyCourseStorage = serviceProvider.GetRequiredService<ICurrencyCourseStorage>();


Console.WriteLine("Start processing:");

await currencyStorage.SaveToStorage(CancellationToken.None);
await currencyCourseStorage.SaveToStorage(CancellationToken.None);

Console.WriteLine("End of the work");