using System;
using Xunit;

namespace TokenGenerator.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData((long)1234567812345678, 123, (long)6785)]
        [InlineData((long)8546, 1, (long)6854)]
        [InlineData((long)2, 12, (long)2)]
        [InlineData((long)4536987123546975, 15478, (long)7569)]
        public void CreateToken_ValidInput_ShouldSucceed(long? cardNumber, int? cvv, long? expected)
        {
            var card = new Card
            {
                CardNumber = cardNumber,
                Cvv = cvv
            };

            // var actual = card.CreateToken();

            // Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateToken_ValidInput_ShouldReturnsTrue()
        {
            var requestCard = new Card
            {
                RegistrationDate = DateTime.UtcNow,
                CustomerId = 1
            };

            var savedCard = new Card
            {
                RegistrationDate = DateTime.UtcNow,
                Customer = new Customer
                {
                    Id = 1
                }
            };

            // var isValid = savedCard.ValidateToken(requestCard);

            // Assert.True(isValid);
        }
    }
}
