using Microsoft.EntityFrameworkCore;
using TokenGenerator.Business;

namespace TokenGenerator.Models
{
    /// <summary>
    /// CustomerCardContext class.
    /// </summary>
    public class CustomerCardContext : DbContext, ICustomerCardContext
    {
        private Validator validator;
        public CustomerCardContext(DbContextOptions<CustomerCardContext> options)
            : base(options)
        {
            this.validator = new Validator();
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
        public IValidator Validator
        {
            get
            {
                return this.validator;
            }

            set => this.validator = new Validator();
        }
    }
}
