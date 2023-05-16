#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace EComerce.Models;
public class Order
{
    [Key]
    public int OrderId {get;set;}
    public int CustomerId {get;set;}
    public Customer? Customer {get;set;}
    public int ProductId {get;set;}
    public Product? Product {get;set;}
    [Range(1, int.MaxValue)]
    public int QuantityBought {get;set;}
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}