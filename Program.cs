using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
//método Main, que inicializa tu aplicación ASP.NET Core
/// <summary>
/// Clase principal que contiene el método de entrada para la aplicación.
/// </summary>
public class Program
{
/// <summary>
/// Punto de entrada principal para la aplicación.
/// </summary>
/// <param name="args">Argumentos de línea de comandos pasados a la aplicación.</param>

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    
/// <summary>
/// Método que crea y configura el host de la aplicación.
/// </summary>
/// <param name="args">Argumentos de línea de comandos pasados a la aplicación.</param>
/// <returns>Una instancia de IHostBuilder configurada para la aplicación.</returns>

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
