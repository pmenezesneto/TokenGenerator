using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TokenGenerator.Controllers
{
    public partial class TokenController : Controller
    {
        [HttpPost("validateToken")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Card>> ValidateToken(Card card)
        {
            if (card == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Card dbCard = await this.customerCardContext.Cards.FindAsync(card.Id);
            dbCard.Customer = await this.customerCardContext.Customers.FindAsync(card.CustomerId);

            if (dbCard == null || dbCard.Customer == null || !dbCard.ValidateToken(card))
            {
                return Unauthorized(new { Validated = false });
            }

            Console.WriteLine(dbCard.CardNumber);

            var tempToken = dbCard.CreateToken();

            return Accepted(new { Validated = tempToken == card.Token });
        }
    }
}
