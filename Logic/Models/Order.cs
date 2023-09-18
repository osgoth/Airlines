namespace Logic.Models;

public class Order
{
    public string OrderId { get; set; }
    public string Destination { get; set; }
    public int FlightNumber { get; set; }
    public int DayNumber { get; set; }
    public string Departure { get; set; }
}