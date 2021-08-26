using System;
using TokenGenerator.Models;

namespace TokenGenerator
{
    /// <summary>
    /// The Business Card Class.
    /// </summary>
    public partial class Card
    {
        /// <summary>
        /// Create a token based in card's number.
        /// </summary>
        /// <returns>The created token.</returns>
        public int CreateToken()
        {
            var arrayNumbers = this.CardNumber.ToString().ToCharArray();

            if (arrayNumbers.Length > 4)
            {
                arrayNumbers = new String(arrayNumbers, (arrayNumbers.Length -4), 4).ToCharArray();
            }

            var digits = Array.ConvertAll(arrayNumbers, c => (int)Char.GetNumericValue(c));

            for (int i = 0; i < this.Cvv; i++)
            {
                int j, last;

                last = digits[digits.Length - 1];

                for (j = digits.Length -1; j > 0; j--)
                {
                    digits[j] = digits[j - 1];
                }

                digits[0] = last;
            }

            return Int32.Parse(string.Join("", digits));
        }

        public bool ValidateToken(Card requestCard)
        {
            return this.RegistrationDate > DateTime.UtcNow.AddMinutes(-30) &&
                this.Customer.Exists(requestCard.CustomerId);
        }
    }
}
