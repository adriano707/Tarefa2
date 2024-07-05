using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Contexts;

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = "server=168.75.101.32;database=db_account_payable2;user id=sa;password=aL58070102;Trusted_connection=false;TrustServerCertificate=True;";
        //builder.UseSqlServer(connectionString);
        return new AppDbContext(builder.Options);
    }
}