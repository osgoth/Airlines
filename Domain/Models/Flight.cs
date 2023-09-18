using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Flight
{
    [Key]
    public int Id { get; set; }
    public int FlightNumber { get; set; }
    public int DayNumber { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }

    public int MaxBoxCapacity { get; set; }
}