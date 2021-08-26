using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TokenGenerator
{
    /// <summary>
    /// Customer model class.
    /// </summary>
    public partial class Customer
    {
        /// <summary>
        /// Gets or sets the customer's identification.
        /// </summary>
        /// <value>The customer identification.</value>
        [Key]
        [JsonProperty("customerId")]
        public int Id { get; set; }
    }
}
