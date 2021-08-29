using Xunit;

namespace TokenGenerator.Tests
{
    public class UnitTestCustomer
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        [InlineData(6, 6)]
        [InlineData(7, 7)]
        public void UnitTest_CustomerExists_ShouldReturnsTrue(int customerId, int value)
        {
            var customer = new Customer
            {
                Id = customerId
            };

            Assert.True(customer.Exists(value));
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 5)]
        [InlineData(5, 6)]
        [InlineData(6, 7)]
        [InlineData(7, 8)]
        public void UnitTest_CustomerExists_ShouldReturnsFalse(int customerId, int value)
        {
            var customer = new Customer
            {
                Id = customerId
            };

            Assert.False(customer.Exists(value));
        }
    }
}