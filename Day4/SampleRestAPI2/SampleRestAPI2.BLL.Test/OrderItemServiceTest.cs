using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.BLL.Test.Common;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Test
{
    public class OrderItemServiceTest
    {

        private IEnumerable<OrdersItems> ordersItems;

        public OrderItemServiceTest()
        {
            ordersItems = CommonHelper.LoadDataFromFile<IEnumerable<OrdersItems>>(@"MockData\OrderItems.json");
        }
    }
}
