using System;

namespace TokenGenerator.Business
{
  public class Validator : IValidator
  {
    public int CreateToken(Card card)
    {
      var arrayNumbers = card.CardNumber.ToString().ToCharArray();

            if (arrayNumbers.Length > 4)
            {
                arrayNumbers = new String(arrayNumbers, (arrayNumbers.Length -4), 4).ToCharArray();
            }

            var digits = Array.ConvertAll(arrayNumbers, c => (int)Char.GetNumericValue(c));

            for (int i = 0; i < card.Cvv; i++)
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

    public bool ValidateToken(Card requestCard, Card savedCard)
    {
      return requestCard.RegistrationDate >= DateTime.UtcNow.AddMinutes(-30) &&
                savedCard.Customer.Exists(requestCard.CustomerId);
    }
  }
}
