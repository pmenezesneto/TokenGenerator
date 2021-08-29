using System;
using TokenGenerator.Business;
using Xunit;

namespace TokenGenerator.Tests
{
    public class UnitTestValidator
    {
        [Fact]
        public void UnitTest_Validator_CreateToken_ShouldReturnsExpected()
        {
            var validator = new Validator();
            var card = new Card
            {
                CardNumber = 1,
                Cvv = 234
            };

            Assert.Equal(1, validator.CreateToken(card));
        }

        [Fact]
        public void UnitTest_Validator_ValidateToken_ShouldReturnsTrue()
        {
            var validator = new Validator();
            var requestCard = new Card
            {
                CustomerId = 1
            };
            var savedCard = new Card
            {
                Customer = new Customer
                {
                    Id = 1
                },
                RegistrationDate = DateTime.UtcNow
            };

            Assert.True(validator.ValidateToken(requestCard, savedCard));
        }

        [Fact]
        public void UnitTest_Validator_ValidateToken_ShouldReturnsFalse()
        {
            var validator = new Validator();
            var requestCard = new Card
            {
                CustomerId = 1
            };
            var savedCard = new Card
            {
                Customer = new Customer
                {
                    Id = 2
                },
                RegistrationDate = DateTime.UtcNow.AddHours(-1)
            };

            Assert.False(validator.ValidateToken(requestCard, savedCard));
        }
    }
}