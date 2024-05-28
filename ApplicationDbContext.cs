using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

/// <summary>
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    /// <summary>
    /// </summary>
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Obtiene la cadena de conexión desde el archivo de configuración appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);
        }
    }    /// <summary>
    /// </summary>
    // DbSet y otras configuraciones
    public DbSet<Trajectory> Trajectories { get; set; }
     /// <summary>
    /// </summary>
    public DbSet<Taxi> Taxis { get; set; }
}
