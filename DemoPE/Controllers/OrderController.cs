using AutoMapper;
using DemoPE.DTOs;
using DemoPE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoPE.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly PrnSum22B1Context _context;
        private readonly IMapper _mapper;

        public OrderController(PrnSum22B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("getallorder")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.Employee.Department)
                .Include(o => o.Customer)
                .ToListAsync();

            var result = _mapper.Map<List<Order>, List<GetOrderDTO>>(orders);
            return Ok(result);
        }

        [Route("getorderbydate/{From}/{To}")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersByDate(DateTime From, DateTime To)
        {
            var orders = await _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.Employee.Department)
                .Include(o => o.Customer)
                .Where(o => o.OrderDate >= From && o.OrderDate <= To)
                .ToListAsync();

            var result = _mapper.Map<List<Order>, List<GetOrderDTO>>(orders);
            return Ok(result);
        }
    }
}

