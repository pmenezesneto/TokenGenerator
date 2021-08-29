using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TokenGenerator.Models;

namespace TokenGenerator.Controllers
{
    /// <summary>
    /// The CustomerCardController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public partial class TokenController : Controller
    {
        private readonly ICustomerCardContext customerCardContext;

        public TokenController(ICustomerCardContext customerCardContext)
        {
            this.customerCardContext = customerCardContext;
        }

        /// <summary>
        /// Persist a card in database
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>An object with</returns>
        [HttpPost("createCard")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Card>> CreateCard(Card card)
        {
            if (card == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var customer = await this.customerCardContext.Customers.FindAsync(card.CustomerId);

                if (customer == null)
                {
                    customer = new Customer
                    {
                        Id = card.CustomerId
                    };

                    this.customerCardContext.Customers.Add(customer);
                    await this.customerCardContext.SaveChangesAsync();
                }

                var existentCard = this.customerCardContext.Cards.Where(ec => ec.CardNumber == card.CardNumber).FirstOrDefault();
                CardResponse cardResponse = new CardResponse();

                if (existentCard == null)
                {
                    card.Customer = customer;
                    Card tempCard = new Card();
                    tempCard.CardNumber = card.CardNumber;
                    tempCard.Cvv = card.Cvv;
                    var token = this.customerCardContext.Validator.CreateToken(tempCard);
                    card.Token = token;

                    this.customerCardContext.Cards.Add(card);
                    await this.customerCardContext.SaveChangesAsync();

                    cardResponse.RegistrationDate = DateTime.Now;
                    cardResponse.Token = card.Token;
                    cardResponse.CardId = card.Id;
                }
                else
                {
                    existentCard.RegistrationDate = DateTime.UtcNow;
                    this.customerCardContext.Update(existentCard);
                    await this.customerCardContext.SaveChangesAsync();

                    cardResponse.RegistrationDate = existentCard.RegistrationDate;
                    cardResponse.Token = existentCard.Token;
                    cardResponse.CardId = existentCard.Id;
                }

                return CreatedAtAction(nameof(CreateCard), cardResponse);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
