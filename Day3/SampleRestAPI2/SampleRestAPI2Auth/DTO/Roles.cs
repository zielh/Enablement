using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleRestAPI2Auth.DTO
{
    public class RolesDTO
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
