namespace Logic.Models;

public class Flight
{
    public int FlightNumber { get; set; }
    public int DayNumber { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }
    public int MaxBoxCapacity { get; set; }
}