using Microsoft.AspNetCore.Authorization;

namespace SampleRestAPI2.Securities
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
