﻿using System.Security.Claims;
using SampleRestAPI2Auth.BLL.Interfaces;
using IdentityModel.Client;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SampleRestAPI2Auth.DAL.Models;
using Microsoft.AspNetCore.Authentication;
using SampleRestAPI2Auth.DAL.Repository;
using SampleRestAPI2Auth.External;
using SampleRestAPI2Auth.External.DTO;

namespace SampleRestAPI2Auth.BLL.Services
{
    public class UserAuthorization : IUserAuthorization
    {
        private readonly ClaimsPrincipal _userPrincipal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IdentityServerApi _identityServerApi;
        private readonly GoogleApi _googleApi;


        public UserAuthorization(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IdentityServerApi identityServerApi, GoogleApi googleApi)
        {
            _unitOfWork = unitOfWork;
            _userPrincipal = httpContextAccessor?.HttpContext?.User;
            _identityServerApi = identityServerApi;
            _googleApi = googleApi;

            MapperConfiguration configMapper = new MapperConfiguration(m =>
            {
                m.CreateMap<GoogleUserInfoDto, User>()
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.email))
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.given_name))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.family_name))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.email));
            });

            _mapper = configMapper.CreateMapper();
        }


        public async Task<TokenResponse> LoginAsync(string userName, string password, bool bypassPassword = false)
        {
            try
            {
                var tokenResponse = await _identityServerApi.AuthWithIdentityServer(userName, password, bypassPassword);

                if (tokenResponse.IsError)
                {
                    throw new Exception(tokenResponse.ErrorDescription);
                }

                return tokenResponse;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public async Task<User> AuthWithGoogleAsync(AuthenticateResult authResult)
        {
            if (authResult?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            var externalUser = authResult.Principal;
            if (externalUser == null)
            {
                throw new Exception("External authentication error");
            }

            var tokenType = authResult.Properties.Items[".Token.token_type"];
            var accessToken = authResult.Properties.Items[".Token.access_token"];

            var resultDto = await _googleApi.AuthWithGoogleAsync(tokenType, accessToken);

            var user = await _unitOfWork.Users.GetBySingle(x => x.Email.ToLower() == resultDto.email.ToLower());

            if (user != null)
            {
                return user;
            }

            user = _mapper.Map<User>(resultDto);

            await _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();

            return user;
        }

        public async Task<User> GetUserAsync(string userName)
        {
            User user = await _unitOfWork.Users.GetBySingle(x => x.UserName.ToLower() == userName.ToLower());
            return user;
        }

        public Guid GetUserId()
        {
            string userId = _userPrincipal.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value;
            return new Guid(userId);
        }

        public string GetUserName()
        {
            string userName = _userPrincipal.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name)?.Value;
            return userName;
        }

        public string GetEmail()
        {
            string userName = _userPrincipal.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Email)?.Value;
            return userName;
        }

        public List<string> GetRole()
        {
            List<string> userName = _userPrincipal.Claims.Where(i => i.Type == ClaimTypes.Role).Select(r => r.Value).ToList();
            return userName;
        }

    }
}
