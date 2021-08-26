using System;
using Newtonsoft.Json;

namespace TokenGenerator.Models
{
    /// <summary>
    /// Card Response class.
    /// </summary>
    public class CardResponse
    {
        /// <summary>
        /// Gets or sets the registration date.
        /// </summary>
        /// <value>The registration date.</value>
        [JsonProperty("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the Token.
        /// </summary>
        /// <value>The token.</value>
        [JsonProperty("token")]
        public int? Token { get; set; }

        /// <summary>
        /// Gets or sets the CardId.
        /// </summary>
        /// <value>The card id.</value>
        [JsonProperty("cardId")]
        public int CardId { get; set; }
    }
}