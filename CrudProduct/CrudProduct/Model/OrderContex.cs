using Microsoft.EntityFrameworkCore;

namespace CrudProduct.Model
{
    public class OrderContex : DbContext
    {
        public OrderContex(DbContextOptions<OrderContex> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
