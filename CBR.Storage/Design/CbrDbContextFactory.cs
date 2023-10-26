using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CBR.Storage.Design;


/// чтобы работали миграции 
/// <see cref="https://learn.microsoft.com/en-gb/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli" />
public class CbrDbContextFactory : IDesignTimeDbContextFactory<CbrDbContext>
{
    public CbrDbContext CreateDbContext(string[] args)
    {
        string connectionString = "User ID=root;Password=admin;Host=localhost;Port=5432;Database=cbr;Pooling=true;";
        
        var options = new DbContextOptionsBuilder<CbrDbContext>()
            .UseNpgsql(connectionString).Options;

        return new CbrDbContext(options);
    }
}
