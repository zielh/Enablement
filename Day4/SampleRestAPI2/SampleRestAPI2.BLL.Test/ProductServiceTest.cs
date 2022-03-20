using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.BLL.Test.Common;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class ProductServiceTest
    {
        private IEnumerable<Products> products;

        public ProductServiceTest()
        {
            products = CommonHelper.LoadDataFromFile<IEnumerable<Products>>(@"MockData\Products.json");
        }
    }
}
