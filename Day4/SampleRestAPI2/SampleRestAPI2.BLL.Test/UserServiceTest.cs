using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class UserServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServiceTest(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}