using Microsoft.AspNetCore.Authorization;

namespace SampleRestAPI2Auth.BLL.Services.DTO
{
    public class Permissions : IAuthorizationRequirement
    {
        public Permissions(string permissionName)
        {
            PermissionName = permissionName;
        }
        public string PermissionName { get; set; }
    }
}
