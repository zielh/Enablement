using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleRestAPI2.DAL.Models
{
    [Table("OrdersItems")]
    public class OrdersItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Orders")]
        [Required]
        public Guid OrderId { get; set; }

        public Orders Orders { get; set; }

        [ForeignKey("Products")]
        [Required]
        public Guid ProductId { get; set; }

        public Products Products { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
