using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleRestAPI2.BLL.Test
{
    public class MerchantServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;

        public MerchantServiceTest(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
