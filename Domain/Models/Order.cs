using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Order
{
    [Key]
    public string OrderId { get; set; }
    public string Destination { get; set; }
}