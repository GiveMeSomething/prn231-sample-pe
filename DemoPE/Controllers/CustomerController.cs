using DemoPE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoPE.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly PrnSum22B1Context _context;

        public CustomerController(PrnSum22B1Context context)
        {
            _context = context;
        }

        [Route("delete/{CustomerId}")]
        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(string CustomerId)
        {
            var foundCustomer = _context.Customers
                .Include(c => c.Orders)
                .Where(c => c.CustomerId == CustomerId)
                .FirstOrDefault();

            if(foundCustomer == null)
            {
                return NotFound();
            }

            var deletedOrders = 0;
            var deletedOrderDetail = 0;
            try
            {
                var orders = foundCustomer.Orders.ToList();
                foreach(Order order in orders)
                {
                    var orderDetails = await _context.OrderDetails
                        .Where(o => o.OrderId == order.OrderId)
                        .ToListAsync();
                    deletedOrderDetail += orderDetails.Count;

                    _context.OrderDetails.RemoveRange(orderDetails);
                    _context.SaveChanges();
                }

                deletedOrders += orders.Count;

                _context.Orders.RemoveRange(orders);
                _context.SaveChanges();

                _context.Customers.Remove(foundCustomer);
                _context.SaveChanges();
            }catch
            {
                return Conflict("There was an unknown error when performing data deletion");
            }

            return Ok(new
            {
                customerDeletedCount = 1,
                orderDeleteCount = deletedOrders,
                orderDetailDeleteCount = deletedOrderDetail
            });
        }
    }
}

