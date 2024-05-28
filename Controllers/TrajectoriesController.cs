using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

// controlador de API en ASP.NET
/// <summary>
/// Controlador para gestionar las operaciones relacionadas con las trayectorias.
/// </summary>
[ApiController] //Decoradores de Clase que se aplican a la clase TrajectoriesController.
[Route("[controller]")] //Decoradores de Clase que se aplican a la clase TrajectoriesController.
public class TrajectoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
//controlador recibe una instancia de ApplicationDbContext.

    /// <summary>
    /// Constructor de la clase TrajectoriesController.
    /// </summary>
    /// <param name="context">Contexto de la base de datos de la aplicación.</param>
  public TrajectoriesController(ApplicationDbContext context)
  {
    _context = context;
  }

  /// <summary>
  /// Obtiene todas las trayectorias disponibles en la base de datos.
  /// </summary>
  [HttpGet] 
  public async Task<ActionResult<IEnumerable<Trajectory>>> GetTrajectories([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
  {
   var trajectories = await _context.Trajectories
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return trajectories;
  }

    /// <summary>
    /// Obtiene todas las ubicaciones de un taxi para una fecha dada.
    /// </summary>
    [HttpGet("locations")]
    public async Task<ActionResult<IEnumerable<Trajectory>>> GetTaxiLocations(
    [FromQuery] int taxiId,
    [FromQuery] string date,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20)
    {
        try
    {
        if (!DateTime.TryParse(date, out DateTime parsedDate))
        {
            return BadRequest("Formato de fecha inválido. Utiliza el formato ISO 8601 (YYYY-MM-DD).");
        }
        DateTime utcDate = parsedDate.ToUniversalTime();

        var locations = await _context.Trajectories
            .Where(t => t.TaxiId == taxiId && t.Date.Date == utcDate.Date) 
            .Select(t => new { Latitude = t.Latitude, Longitude = t.Longitude, Date = t.Date })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (locations.Count == 0)
        {
            return NotFound("No se encontraron ubicaciones para el taxi y la fecha especificados.");
        }

        return Ok(locations);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    }
    }
}