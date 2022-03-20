using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SampleRestAPI2.DAL.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        public long Price { get; set; }

        [ForeignKey("Merchants")]
        [Required]
        public Guid MerchantId { get; set; }

        public Merchants Merchants { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
