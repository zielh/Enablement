using Microsoft.EntityFrameworkCore;
using Moq;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.BLL.Services;
using SampleRestAPI2.BLL.Test.Common;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleRestAPI2.BLL.Test
{
    public class MerchantServiceTest
    {

        private IEnumerable<Merchants> merchants;

        public MerchantServiceTest()
        {
            merchants = CommonHelper.LoadDataFromFile<IEnumerable<Merchants>>(@"MockData\Merchants.json");
        }
    }
}
