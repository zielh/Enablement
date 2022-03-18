using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SampleRestAPI2.DAL.Models
{
    [Table("Countries")]
    public class Countries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Merchants> Merchants { get; set; }

    }
}
