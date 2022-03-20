using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class OrderServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderServiceTest(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
