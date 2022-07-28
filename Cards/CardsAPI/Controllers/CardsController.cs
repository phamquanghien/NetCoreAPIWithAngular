namespace CardsAPI.Controllers
{
    using CardsAPI.Data;
    using CardsAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly CardsDbContext _dbContext;
        public CardsController(CardsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //get all cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _dbContext.Cards.ToListAsync();
            return Ok(cards);
        }
        // get single cards
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await _dbContext.Cards.FirstOrDefaultAsync(m => m.Id == id);
            if (card!=null)
            {
                return Ok(card);
            }
            return NotFound("Card not found");
        }
        // add card
        [HttpPost]
        public async Task<IActionResult> AddCards([FromBody] Card card)
        {
            card.Id = new Guid();
            await _dbContext.Cards.AddRangeAsync(card);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard),new {id = card.Id}, card);
        }
        // update a card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await _dbContext.Cards.FirstOrDefaultAsync(m => m.Id == id);
            if(existingCard != null)
            {
                existingCard.CardholderName = card.CardholderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CVC = card.CVC;
                await _dbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card not found");
        }
        // delete a card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await _dbContext.Cards.FirstOrDefaultAsync(m => m.Id == id);
            if(existingCard != null)
            {
                _dbContext.Remove(existingCard);
                await _dbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card not found");
        }
    }
}