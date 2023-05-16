#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace EComerce.Models;
public class Product
{
    [Key]
    public int ProductId {get;set;}

    [Required(ErrorMessage = "Name is required")]
    public string Name {get;set;}

    [Required(ErrorMessage = "Image is required")]
    public string Image {get;set;}

    [Required(ErrorMessage = "Quantity is required")]
    [Range(0,int.MaxValue)]
    public int Quantity {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    
    public List<Order> OrdersIn {get;set;} = new List<Order>();
}