using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TokenGenerator
{
    /// <summary>
    /// Card model class.
    /// </summary>
    public partial class Card
    {
        /// <summary>
        /// Gets or sets the card's identification.
        /// </summary>
        /// <value>The card's identification.</value>
        [Key]
        [JsonProperty("cardId")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the card's customer identification.
        /// </summary>
        /// <value>The card's customer identification.</value>
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the card's Customer object.
        /// </summary>
        /// <value>The card's Customer object</value>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the card's number.
        /// </summary>
        /// <value>The card's number.</value>
        [Range(1, 9999999999999999, ErrorMessage = "The Card Number must have a range between 1 and 16 digits.")]
        public long? CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the CVV.
        /// </summary>
        /// <value>The cvv.</value>
        [Range(1, 99999, ErrorMessage = "The CVV must have a range between 1 and 5 digits.")]
        public int? Cvv { get; set; }

        /// <summary>
        /// Gets or sets the registration date.
        /// </summary>
        /// <value>The registration date.</value>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the Token.
        /// </summary>
        /// <value>The token.</value>
        public int? Token { get; set; }
    }
}
