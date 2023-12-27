using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudProduct.Model
{
    [Table("order")]
    public class Order
    {
        [Key]
        public int id { get; set; }
        [Column("created_at")]
        public DateTime created_at { get; set; }
    }
}
