using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleRestAPI2.DAL.Models
{
    [Table("Logs")]
    public class Logs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
