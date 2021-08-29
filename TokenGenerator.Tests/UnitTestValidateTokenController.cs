using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TokenGenerator.Business;
using TokenGenerator.Controllers;
using TokenGenerator.Models;
using Xunit;

namespace TokenGenerator.Tests
{
    public class UnitTestValidateTokenController
    {
        [Fact]
        public void UnitTest_ValidateToken_InvalidCard_ShouldReturnsBadRequest()
        {
            
            var options = new DbContextOptionsBuilder<CustomerCardContext>()
                .UseInMemoryDatabase("CashlessRegistrator")
                .Options;
            var context = new CustomerCardContext(options);
            var controller = new TokenController(context);

            var response = controller.ValidateToken(null);

            Assert.IsType<BadRequestResult>(response.Result.Result);
        }

        [Fact]
        public async void UnitTest_ValidateToken_InvalidData_ShouldsReturnUnauthorized()
        {
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

            var fakeCardsDbSet = Globals.MockDbSetWrongResult(fakeCardsData);
            var fakeCustomerDbSet = Globals.MockDbSet(fakeCustomerData);

            var moqCustomerCardContext = new Moq.Mock<ICustomerCardContext>();
            var moqValidate = new Moq.Mock<IValidator>();
            var moqIQuerybleCard = new System.Collections.Generic.List<Card>();

            moqCustomerCardContext.Setup(cardContext => cardContext.Cards).Returns(fakeCardsDbSet.Object);
            moqCustomerCardContext.Setup(customerContext => customerContext.Customers).Returns(fakeCustomerDbSet.Object);

            var controller = new TokenController(moqCustomerCardContext.Object);
            var response = await controller.ValidateToken(fakeCard);

            var result = (UnauthorizedObjectResult)response.Result;

            Assert.IsType<UnauthorizedObjectResult>(response.Result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async void UnitTest_ValidateToken_ValidData_ShouldReturnsAccepted()
        {
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
            var fakeCustomer = new Customer
            {
                Id = 1
            };

            var fakeCardsDbSet = MockDbSetFindAsync(fakeCardsData, fakeCard);
            var fakeCustomerDbSet = MockDbSetFindAsync(fakeCustomerData, fakeCustomer);

            var moqCustomerCardContext = new Moq.Mock<ICustomerCardContext>();
            var moqValidate = new Moq.Mock<IValidator>();

            moqValidate.Setup(validate => validate.ValidateToken(It.IsAny<Card>(), It.IsAny<Card>())).Returns(true);
            moqCustomerCardContext.Setup(cardContext => cardContext.Cards).Returns(fakeCardsDbSet.Object);
            moqCustomerCardContext.Setup(customerContext => customerContext.Customers).Returns(fakeCustomerDbSet.Object);
            moqCustomerCardContext.Setup(customerCardContext => customerCardContext.Validator).Returns(moqValidate.Object);

            var controller = new TokenController(moqCustomerCardContext.Object);
            var response = await controller.ValidateToken(fakeCard);

            var result = (AcceptedResult)response.Result;

            Assert.IsType<AcceptedResult>(response.Result);
            Assert.Equal(202, result.StatusCode);
        }

        private Mock<DbSet<T>> MockDbSetFindAsync<T>(IEnumerable<T> list, dynamic value) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.Setup(x => x.FindAsync(It.IsAny<int>())).Returns(new ValueTask<T>(value));

            return dbSetMock;
        }
    }
}