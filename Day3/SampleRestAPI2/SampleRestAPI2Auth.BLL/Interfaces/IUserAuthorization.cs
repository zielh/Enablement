using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using SampleRestAPI2Auth.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleRestAPI2Auth.BLL.Interfaces
{
    public interface IUserAuthorization
    {
        Task<TokenResponse> LoginAsync(string userName, string password, bool autopassword = false);
        Task<User> AuthWithGoogleAsync(AuthenticateResult authResult);
        Task<User> GetUserAsync(string userName);
        Guid GetUserId();
        string GetUserName();
        string GetEmail();
        List<string> GetRole();
    }
}
