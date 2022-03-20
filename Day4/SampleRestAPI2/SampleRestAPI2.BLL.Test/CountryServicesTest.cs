using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockQueryable.Moq;
using Moq;
using SampleRestAPI2.BLL.Services;
using SampleRestAPI2.BLL.Test.Common;
using SampleRestAPI2.DAL.Models;
using SampleRestAPI2.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SampleRestAPI2.BLL.Test
{
    public class CountryServicesTest
    {
        private IEnumerable<Countries> countries;
        private Mock<IUnitOfWork> uow;

        public CountryServicesTest()
        {
            countries = CommonHelper.LoadDataFromFile<IEnumerable<Countries>>(@"MockData\Countries.json");
            uow = MockUnitOfWork();
        }

        private CountryServices CreateCountryService()
        {
            return new CountryServices(uow.Object);
        }


        private Mock<IUnitOfWork> MockUnitOfWork()
        {
            var countriesQueryable = countries.AsQueryable().BuildMock().Object;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(u => u.Countries.GetAll())
                .Returns(countriesQueryable);

            mockUnitOfWork
                .Setup(u => u.Countries.GetBy(It.IsAny<Expression<Func<Countries, bool>>>()))
                .ReturnsAsync((Expression<Func<Countries, bool>> condition) => countriesQueryable.Where(condition).AsEnumerable());

            mockUnitOfWork
               .Setup(u => u.Countries.GetBySingle(It.IsAny<Expression<Func<Countries, bool>>>()))
               .ReturnsAsync((Expression<Func<Countries, bool>> condition) => countriesQueryable.FirstOrDefault(condition));

            mockUnitOfWork
               .Setup(u => u.Countries.Add(It.IsAny<Countries>()))
               .ReturnsAsync((Countries countries) =>
               {
                   countries.Id = Guid.NewGuid();
                   return countries;
               });

            mockUnitOfWork
               .Setup(u => u.Countries.Delete(It.IsAny<Countries>()))
               .Returns((Countries countries) =>
               {
                   countries.Id = Guid.NewGuid();
                   return countries;
               });


            mockUnitOfWork
                .Setup(x => x.Countries.Update(It.IsAny<Countries>()))
               .Returns((Countries countries) =>
               {
                   countries.Id = Guid.NewGuid();
                   return countries;
               });

            return mockUnitOfWork;
        }

        [Fact]
        public async Task GetAsync_Success()
        {
            //arrange
            var expected = countries;

            var svc = CreateCountryService();

            // act
            var actual = await svc.Get();

            // assert      
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("f38edc0c-3f8c-434d-361c-08da0a764d88")]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6")]
        public async Task GetSingle_Success(string countryId)
        {
            //arrange
            var id = Guid.Parse(countryId);
            var expected = countries.First(x => x.Id == id);

            var svc = CreateCountryService();

            //act
            var actual = await svc.GetSingle(x => id == x.Id);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("3fa85f64-5717-4562-b3fc-2c963f66afa6")]
        public async Task GetWithMerchants_Success(string countryId)
        {
            //arrange
            var id = Guid.Parse(countryId);
            var expected = countries.First(x => x.Id == id);

            var svc = CreateCountryService();

            //act
            var actual = await svc.GetWithMerchants(x => id == x.Id);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task CreateCountries_Success()
        {
            //arrange
            var expected = new Countries
            {
                Id = Guid.Parse("5d455f7c-b31c-4ae8-a856-5281d830bdcd"),
                Name = "Brunei"
            };

            var svc = CreateCountryService();

            //act
            Func<Task> act = async () => { await svc.Add(expected); };

            //assert
            await act.Should().NotThrowAsync<Exception>();
            uow.Verify(x => x.Complete());
        }

        [Theory]
        [InlineData("f38edc0c-3f8c-434d-361c-08da0a764d88", "Myanmar")]
        public async Task UpdateUser_Success(string id, string name)
        {
            //arrange
            var expected = new Countries
            {
                Id = Guid.Parse(id),
                Name = name
            };

            var svc = CreateCountryService();

            //act
            bool act = svc.Update(expected);

            //assert
            act.Should().BeTrue();
        }

        [Theory]
        [InlineData("51ad40de-6557-4cb7-87e1-672ec4ad4c9e", "Laos")]
        public async Task UpdateUser_NotFoundAsync(string id, string name)
        {
            //arrange
            var expected = new Countries
            {
                Id = Guid.Parse(id),
                Name = name
            };

            var svc = CreateCountryService();

            //act
            bool act = svc.Update(expected);

            //assert
            act.Should().BeFalse();
        }

        [Theory]
        [InlineData("f38edc0c-3f8c-434d-361c-08da0a764d88")]
        public async Task DeleteUser_Success(string countryId)
        {
            //arrange
            var id = Guid.Parse(countryId);

            var svc = CreateCountryService();

            //act
            bool act = svc.Delete(id);

            //assert
            act.Should().BeTrue();
        }
    }
}
