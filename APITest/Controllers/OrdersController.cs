using System.Diagnostics;
using System.Threading.Tasks;
using APITest.Data;
using APITest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// DTO สำหรับ Requests และ Responses
public class CreateOrderRequest
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderResponse
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsShipped { get; set; }
}


[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST api/orders
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var customer = await _context.Customers.FindAsync(request.CustomerId);
        if (customer == null)
        {
            return BadRequest("Invalid Customer ID.");
        }

        var product = await _context.Products.FindAsync(request.ProductId);
        if (product == null)
        {
            return BadRequest("Invalid Product ID.");
        }

        Console.WriteLine($"Received CustomerID: {request.CustomerId}, ProductID: {request.ProductId}");

        var order = new Order
        {
            CustomerID = request.CustomerId,
            ProductID = request.ProductId,
            Quantity = request.Quantity,
            OrderDate = DateTime.UtcNow,
            IsShipped = false
        };

        // Add Watch
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        Debug.WriteLine($"Order saved to database. New OrderID is: {order.OrderID}");


        var response = new OrderResponse
        {
            OrderId = order.OrderID,
            CustomerName = $"{customer.FirstName} {customer.LastName}",
            ProductName = product.ProductName,
            ProductPrice = product.Price,
            OrderDate = order.OrderDate,
            IsShipped = order.IsShipped
        };

        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, response);
    }

    // GET api/orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> GetOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Product)
            .FirstOrDefaultAsync(o => o.OrderID == id);

        if (order == null)
        {
            return NotFound();
        }

        var response = new OrderResponse
        {
            OrderId = order.OrderID,
            CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}",
            ProductName = order.Product.ProductName,
            ProductPrice = order.Product.Price,
            OrderDate = order.OrderDate,
            IsShipped = order.IsShipped
        };

        return Ok(response);
    }
}