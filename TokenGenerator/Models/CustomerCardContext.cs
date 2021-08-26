using Microsoft.EntityFrameworkCore;

namespace TokenGenerator.Models
{
    /// <summary>
    /// CustomerCardContext class.
    /// </summary>
    public class CustomerCardContext : DbContext
    {
        public CustomerCardContext(DbContextOptions<CustomerCardContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbCardContext to a list of Cards.
        /// </summary>
        /// <value>The DbCardContext to a list of Cards.</value>
        public DbSet<Card> Cards { get; set; }

        /// <summary>
        /// Gets or sets the DbCardContext to a list of Customers.
        /// </summary>
        /// <value>The DbCardContext to a list of customers.</value>
        public DbSet<Customer> Customers { get; set; }
    }
}
