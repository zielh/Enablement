using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleRestAPI2.DAL.Models
{
    [Table("Merchants")]
    public class Merchants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [ForeignKey("Users")]
        [Required]
        public Guid UserId { get; set; }

        public Users Users { get; set; }

        [ForeignKey("Countries")]
        [Required]
        public Guid CountryId { get; set; }

        public Countries Countries { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
