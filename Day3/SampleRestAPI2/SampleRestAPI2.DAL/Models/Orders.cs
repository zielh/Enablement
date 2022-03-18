using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleRestAPI2.DAL.Models
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Users")]
        [Required]
        public Guid UserId { get; set; }

        public Users Users { get; set; }

        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
