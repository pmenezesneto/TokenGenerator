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
            var moqCustomerCardContext = new Moq.Mock<ICustomerCardContext>();
            var moqValidate = new Moq.Mock<IValidator>();
            var fakeCard = new Card
            {
                CardNumber = 5687921,
                Cvv = 14
            };
            
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

            var fakeCardsDbSet = Globals.MockDbSet(fakeCardsData);
            var fakeCustomerDbSet = Globals.MockDbSet(fakeCustomerData);

            moqValidate.Setup(validate => validate.CreateToken(It.IsAny<Card>())).Returns(2);
            moqCustomerCardContext.Setup(customerCardContext => customerCardContext.Validator).Returns(moqValidate.Object);
            moqCustomerCardContext.Setup(cardContext => cardContext.Cards).Returns(fakeCardsDbSet.Object);
            moqCustomerCardContext.Setup(customerContext => customerContext.Customers).Returns(fakeCustomerDbSet.Object);
            
            var controller = new TokenController(moqCustomerCardContext.Object);

            var response = await controller.CreateCard(fakeCard);
            var result = (CreatedAtActionResult)response.Result;

            Assert.IsType<CreatedAtActionResult>(response.Result);
            Assert.Equal(2, ((CardResponse)result.Value).Token);
        }

        [Fact]
        public async void CreatToken_ExistentCard_ShouldReturnsValidObject()
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

            var fakeCardsDbSet = Globals.MockDbSet(fakeCardsData);
            var fakeCustomerDbSet = Globals.MockDbSet(fakeCustomerData);

            var moqCustomerCardContext = new Moq.Mock<ICustomerCardContext>();
            var moqValidate = new Moq.Mock<IValidator>();

            moqCustomerCardContext.Setup(cardContext => cardContext.Cards).Returns(fakeCardsDbSet.Object);
            moqCustomerCardContext.Setup(customerContext => customerContext.Customers).Returns(fakeCustomerDbSet.Object);
            
            var controller = new TokenController(moqCustomerCardContext.Object);

            var response = await controller.CreateCard(fakeCard);
            var result = (CreatedAtActionResult)response.Result;

            Assert.IsType<CreatedAtActionResult>(response.Result);
            Assert.Null(((CardResponse)result.Value).Token);
        }

        [Fact]
        public void UnitTest_CreateToken_InvalidCard_ShouldReturnsBadRequest()
        {
            
            var options = new DbContextOptionsBuilder<CustomerCardContext>()
                .UseInMemoryDatabase("CashlessRegistrator")
                .Options;
            var context = new CustomerCardContext(options);
            var controller = new TokenController(context);

            var response = controller.CreateCard(null);

            Assert.IsType<BadRequestResult>(response.Result.Result);
        }

        [Fact]
        public void UnitTet_CreteToken_ThrowsException_ShouldReturnsInternalServerError()
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

            var fakeCardsDbSet = Globals.MockDbSet(fakeCardsData);
            var fakeCustomerDbSet = Globals.MockDbSet(fakeCustomerData);

            var moqCustomerCardContext = new Moq.Mock<ICustomerCardContext>();
            var moqValidate = new Moq.Mock<IValidator>();

            moqCustomerCardContext.Setup(cardContext => cardContext.Cards).Throws(new Exception ("Test exception throws."));
            
            var controller = new TokenController(moqCustomerCardContext.Object);

            var response = controller.CreateCard(fakeCard);
            var objectResult = (ObjectResult)response.Result.Result;

            Assert.IsType<ObjectResult>(response.Result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
