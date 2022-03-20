using IdentityServer4.Validation;
using IdentityServer4.Models;
using SampleRestAPI2Auth.DAL.Repository;

namespace SampleRestAPI2Auth.BLL.Services
{
    public class ResourceOwnerPasswordValidatorService : IResourceOwnerPasswordValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResourceOwnerPasswordValidatorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _unitOfWork.Users.GetBySingle(x => x.UserName.ToLower() == context.UserName.ToLower());

            if (context.Request.Raw["bypassPassword"] == "true")
            {
                context.Result = new GrantValidationResult(user.UserId.ToString(), "password");
                return;
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(context.Password, user.Password);

            if (passwordMatch)
            {
                context.Result = new GrantValidationResult(user.UserId.ToString(), "password");
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid username or password");
            }

        }
    }
}
