#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace EComerce.Models;
public class Customer
{
    [Key]
    public int CustomerId {get;set;}

    [Required(ErrorMessage = "Name is required")]
    public string Name {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    
    public List<Order> OrdersMade {get;set;} = new List<Order>();
}