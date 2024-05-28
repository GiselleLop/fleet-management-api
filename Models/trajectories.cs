using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// Clase que representa una trayectoria.
/// </summary>
[Table("trajectories")]
public class Trajectory
{
    /// <summary>
    /// Identificador único de la trayectoria.
    /// </summary>
    [Column("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Identificador único del taxi asociado a la trayectoria.
    /// </summary>
    [Column("taxi_id")]
    public int TaxiId { get; set; }

    /// <summary>
    /// Fecha y hora de la trayectoria.
    /// </summary>
    [Column("date")]
    public DateTime Date { get; set; }
    /// <summary>
    /// Latitud de la ubicación de la trayectoria.
    /// </summary>
    [Column("latitude")]
    public double Latitude { get; set; }
    /// <summary>
    /// Longitud de la ubicación de la trayectoria.
    /// </summary>
    [Column("longitude")]
    public double Longitude { get; set; }
}
