#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace EComerce.Models;
public class MyContext : DbContext 
{
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<Customer> Customers {get;set;}
    public DbSet<Product> Products {get;set;}
    public DbSet<Order> Orders {get;set;}
}