using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudProduct.Model
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("unit_price")]
        public string unit_price { get; set; }
    }
}
