using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controlador para gestionar las operaciones relacionadas con taxis.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TaxisController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Constructor de la clase TaxisController.
    /// </summary>
    /// <param name="context">Contexto de la base de datos de la aplicación.</param>
    public TaxisController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas los taxis disponibles en la base de datos.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Taxi>>> GetTaxis([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var taxis = await _context.Taxis
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return taxis;
    }

    /// <summary>
    /// Obtiene un taxi por su ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Taxi>> GetTaxi(int id)
    {
        var taxi = await _context.Taxis.FindAsync(id);

        if (taxi == null)
        {
            return NotFound();
        }

        return taxi;
    }

    /// <summary>
    /// Obtiene un taxi por su placa.
    /// </summary>
    [HttpGet("byPlate/{plate}")]
    public async Task<ActionResult<Taxi>> GetTaxiByPlate(string plate)
    {
        var taxi = await _context.Taxis.FirstOrDefaultAsync(t => t.Plate == plate);

        if (taxi == null)
        {
            return NotFound();
        }

        return taxi;
    }




    /// <summary>
        /// Obtiene la última ubicación reportada por un taxi.
        /// </summary>
        /// <param name="taxiId">El ID del taxi.</param>
        /// <returns>La última ubicación del taxi especificado junto con su placa.</returns>
        [HttpGet("lastLocation/{taxiId}")]
        public async Task<ActionResult<object>> GetLastLocation(int taxiId)
        {
            try
            {
                var lastLocation = await (from trajectory in _context.Trajectories
                                          join taxi in _context.Taxis on trajectory.TaxiId equals taxi.Id
                                          where trajectory.TaxiId == taxiId
                                          orderby trajectory.Date descending
                                          select new
                                          {
                                              TaxiId = trajectory.TaxiId,
                                              LicensePlate = taxi.Plate,
                                              Latitude = trajectory.Latitude,
                                              Longitude = trajectory.Longitude,
                                              Date = trajectory.Date,
                                             
                                          }).FirstOrDefaultAsync();

                if (lastLocation == null)
                {
                    return NotFound("No se encontró la última ubicación para el taxi especificado.");
                }

                return Ok(lastLocation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

}
