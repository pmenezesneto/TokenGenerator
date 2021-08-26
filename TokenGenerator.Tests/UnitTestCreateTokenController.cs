using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .UseInMemoryDatabase("CashlessRegistratorGT")
                .Options;
                
            var context = new CustomerCardContext(options);

            var controller = new TokenController(context);
            var card = new Card
            {
                CardNumber = 5687921,
                Cvv = 14,
                CustomerId = 1
            };

            var response = await controller.CreateCard(card);

            Assert.IsType<CreatedAtActionResult>(response.Result);
        }
    }
}