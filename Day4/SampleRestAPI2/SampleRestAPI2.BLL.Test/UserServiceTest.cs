using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.BLL.DTO;
using SampleRestAPI2.BLL.Test.Common;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SampleRestAPI2.BLL.Services
{
    public class UserServiceTest
    {
        private IEnumerable<Users> users;

        public UserServiceTest()
        {
            users = CommonHelper.LoadDataFromFile<IEnumerable<Users>>(@"MockData\Users.json");
        }

    }
}