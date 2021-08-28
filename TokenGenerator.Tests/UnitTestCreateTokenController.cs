using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TokenGenerator.Business;
using TokenGenerator.Controllers;
using TokenGenerator.Models;
using Xunit;

namespace TokenGenerator.Tests
{
    public class UnitTestCreateTokenController
    {
        [Fact]
        public async void CreateToken_ValidInput_ShouldReturnsValidObject()
        {
            var options = new DbContextOptionsBuilder<CustomerCardContext>()
                .UseInMemoryDatabase("CashlessRegistrator")
                .Options;

            var moqValidate = new Moq.Mock<IValidator>();
            var card = new Card
            {
                CardNumber = 5687921,
                Cvv = 14
            };
            var context = new CustomerCardContext(options);

            moqValidate.Setup(validate => validate.CreateToken(It.IsAny<Card>())).Returns(2);

            var controller = new TokenController(context, moqValidate.Object);

            var response = await controller.CreateCard(card);
            var result = (CreatedAtActionResult)response.Result;

            Assert.IsType<CreatedAtActionResult>(response.Result);
            Assert.Equal(2, ((CardResponse)result.Value).Token);
        }

        [Fact]
        public async void CreatToken_ExistentCard_ShouldReturnsValidObject()
        {
            var options = new DbContextOptionsBuilder<CustomerCardContext>()
                .UseInMemoryDatabase("CashlessRegistrator")
                .Options;
            var fakeCardsData = new List<Card>
            {
                new Card
                {
                    CardNumber = 654987,
                    CustomerId = 1,
                    Cvv = 23
                }
            };
            var fakeCustomerData = new List<Customer>
            {
                new Customer
                {
                    Id = 1
                }
            };
            var fakeCard = new Card
            {
                CardNumber = 654987,
                CustomerId = 1,
                Cvv = 23
            };

            var fakeCardsDbSet = MockDbSet(fakeCardsData);
            var fakeCustomerDbSet = MockDbSet(fakeCustomerData);

            var moqCustomerCardContext = new Moq.Mock<IDbContext>();
            var moqValidate = new Moq.Mock<IValidator>();
            var moqIQuerybleCard = new System.Collections.Generic.List<Card>();

            moqCustomerCardContext.Setup(cardContext => cardContext.Cards).Returns(fakeCardsDbSet.Object);
            moqCustomerCardContext.Setup(customerContext => customerContext.Customers).Returns(fakeCustomerDbSet.Object);
            
            var controller = new TokenController(moqCustomerCardContext.Object, moqValidate.Object);

            var response = await controller.CreateCard(fakeCard);
            var result = (CreatedAtActionResult)response.Result;

            Assert.IsType<CreatedAtActionResult>(response.Result);
            Assert.Null(((CardResponse)result.Value).Token);
        }

        Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new() {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());
            dbSetMock.Setup(x => x.FindAsync()).Returns(new System.Threading.Tasks.ValueTask<T>());
            dbSetMock.Setup(x => x.Add(It.IsAny<T>())).Callback<T>((s) => queryableList.Append(s));

            return dbSetMock;
        }
    }
}
