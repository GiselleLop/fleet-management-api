using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

/// <summary>
/// Clase que representa un taxi.
/// </summary>
[Table("taxis")]
public class Taxi
{
    /// <summary>
    /// Identificador único del taxi.
    /// </summary>
    [Column("id")]
    public int Id { get; set; }
    /// <summary>
    /// Placa del taxi.
    /// </summary>
   
    [Column("plate")]
    public string Plate { get; set; }
}
