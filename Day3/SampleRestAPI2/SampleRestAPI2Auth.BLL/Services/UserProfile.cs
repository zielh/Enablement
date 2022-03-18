using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using SampleRestAPI2Auth.DAL.Models;
using SampleRestAPI2Auth.DAL.Repository;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SampleRestAPI2Auth.BLL.Services
{
    public class UserProfile : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProfile(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            Guid userId = new Guid(context.Subject.GetSubjectId());
            List<Claim> claims = new List<Claim>();

            var user = await _unitOfWork.Users.GetAll().Include(x => x.UserRoles).Where(x => x.UserId == userId)
                .Select(usr => new
                {
                    UserName = usr.UserName,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    Email = usr.Email,
                    Roles = usr.UserRoles.Select(role => role.Role.RoleCode)
                }).SingleAsync();

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (string role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            User user = await _unitOfWork.Users.GetBySingle(x => x.UserId == new Guid(sub));
            if (user != null) context.IsActive = true;
        }
    }
}
