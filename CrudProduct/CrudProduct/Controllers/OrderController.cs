using CrudProduct.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderContex orderContex;
        public OrderController(OrderContex _orderContex)
        {
            orderContex = _orderContex;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrder()
        {
            return orderContex.Orders;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await orderContex.Orders.FindAsync(id);
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            orderContex.Orders.Add(order);
            await orderContex.SaveChangesAsync();
            return CreatedAtAction(nameof(Order), new {id = order.id}, order);
        }

        [HttpPut]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            orderContex.Entry(order).State = EntityState.Modified;
            try
            {
                await orderContex.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool OrderAvailable(int id)
        {
            return (orderContex.Orders?.Any(x => x.id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await orderContex.Orders.FindAsync(id);
            orderContex.Orders.Remove(order);
            await orderContex.SaveChangesAsync();
            return Ok();
        }
    }
}
