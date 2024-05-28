using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


/// <summary>
/// Clase de inicio de la aplicación.
/// </summary>
public class Startup
{
  private readonly IConfiguration _configuration;
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Configura los servicios de la aplicación.
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    public void ConfigureServices(IServiceCollection services)
    {
     services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
      
    //    services.AddScoped<ApplicationDbContext>();
        services.AddControllers();

        // Configuración de Swashbuckle
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Documentation",
                Version = "v1",
                Description = "API Documentation for My Application"
            });
            
            // Incluir comentarios XML para documentación de controlador
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }

    /// <summary>
    /// Configura la aplicación y el pipeline de solicitud HTTP.
    /// </summary>
    /// <param name="app">Constructor de la aplicación.</param>
    /// <param name="env">Entorno de hospedaje de la aplicación.</param>
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        // Middleware de Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation V1");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
