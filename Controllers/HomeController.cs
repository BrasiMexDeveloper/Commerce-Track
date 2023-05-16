using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EComerce.Models;
using Microsoft.EntityFrameworkCore;
using EComerce.Models;

namespace ECommerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        MyViewModel myModel = new MyViewModel()
        {
            AllCustomers = _context.Customers.Take(4).ToList(),
            AllProducts = _context.Products.Take(4).ToList(),
            AllOrders = _context.Orders.Include(c => c.Customer).Include(p => p.Product).ToList()
        };
        return View(myModel);
    }

    [HttpGet("products")]
    public IActionResult Products()
    {
        MyViewModel MyModel = new MyViewModel()
        {
            AllProducts = _context.Products.ToList()
        };
        return View(MyModel);
    }

    [HttpPost("products/create")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Products");
        } else {
            MyViewModel MyModel = new MyViewModel()
            {
                AllProducts = _context.Products.ToList()
            };
            return View("Products", MyModel);
        }
    }

    [HttpGet("orders")]
    public IActionResult Orders()
    {
        ViewBag.AllCustomers = _context.Customers.ToList();
        ViewBag.AllProducts = _context.Products.ToList();
        MyViewModel MyModel = new MyViewModel()
        {
            AllOrders = _context.Orders.Include(c => c.Customer).Include(p => p.Product).ToList()
        };
        return View(MyModel);
    }

    [HttpPost("orders/create")]
    public IActionResult CreateOrder(Order newOrder)
    {
        if(ModelState.IsValid)
        {
            Product? ProductBought = _context.Products.FirstOrDefault(p => p.ProductId == newOrder.ProductId);
            if(ProductBought == null)
            {
                return RedirectToAction("Index");
            }
            if(ProductBought.Quantity < newOrder.QuantityBought)
            {
                ModelState.AddModelError("QuantityBought", "Not enough product in stock!");
                ViewBag.AllCustomers = _context.Customers.ToList();
                ViewBag.AllProducts = _context.Products.ToList();
                MyViewModel MyModel = new MyViewModel()
                {
                    AllOrders = _context.Orders.Include(c => c.Customer).Include(p => p.Product).ToList()
                };
                return View("Orders", MyModel);
            }
            ProductBought.Quantity = ProductBought.Quantity - newOrder.QuantityBought;
            _context.Add(newOrder);
            _context.SaveChanges();
            return RedirectToAction("Orders");
        } else {
            ViewBag.AllCustomers = _context.Customers.ToList();
            ViewBag.AllProducts = _context.Products.ToList();
            MyViewModel MyModel = new MyViewModel()
            {
                AllOrders = _context.Orders.Include(c => c.Customer).Include(p => p.Product).ToList()
            };
            return View("Orders", MyModel);
        }
    }

    [HttpGet("customers")]
    public IActionResult Customers()
    {
        MyViewModel MyModel = new MyViewModel()
        {
            AllCustomers = _context.Customers.ToList()
        };
        return View(MyModel);
    }

    [HttpPost("customers/create")]
    public IActionResult CreateCustomer(Customer newCustomer)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newCustomer);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        } else {
            MyViewModel MyModel = new MyViewModel()
            {
                AllCustomers = _context.Customers.ToList()
            };
            return View("Customers", MyModel);
        }
    }

    [HttpPost("customers/destroy/{custId}")]
    public IActionResult DestroyCustomer(int custId)
    {
        Customer? customerToDelete = _context.Customers.SingleOrDefault(c => c.CustomerId == custId);
        _context.Remove(customerToDelete);
        _context.SaveChanges();
        return RedirectToAction("Customers");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}