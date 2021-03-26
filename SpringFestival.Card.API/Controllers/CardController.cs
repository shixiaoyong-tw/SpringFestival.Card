using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpringFestival.Card.Service;
using SpringFestival.Card.UICommand;
using SpringFestival.Card.ViewModel;

namespace SpringFestival.Card.API.Controllers
{
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CardViewModel>>> GetCards()
        {
            var cards = await _cardService.GetAll();

            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardViewModel>> GetCard(Guid id)
        {
            var card = await _cardService.Get(id);

            return Ok(card);
        }

        [HttpGet("votes")]
        public async Task<ActionResult<List<CardVoteViewModel>>> GetVoteResult()
        {
            var voteResult = await _cardService.GetVoteResult();

            return Ok(voteResult);
        }

        [HttpPut]
        public async Task<ActionResult> PutCard(CardEditUICommand command)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = new List<KeyValuePair<string, string>>();
                foreach (var key in ModelState.Keys)
                {
                    errorMessages.AddRange(ModelState[key].Errors
                        .Select(error => new KeyValuePair<string, string>(key, error.ErrorMessage)));
                }

                return BadRequest(errorMessages);
            }

            await _cardService.Edit(command);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> PostCard(CardAddUICommand command)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = new List<KeyValuePair<string, string>>();
                foreach (var key in ModelState.Keys)
                {
                    errorMessages.AddRange(ModelState[key].Errors
                        .Select(error => new KeyValuePair<string, string>(key, error.ErrorMessage)));
                }

                return BadRequest(errorMessages);
            }

            await _cardService.Add(command);

            return Created(new Uri($"{Request.Path}", UriKind.Relative), null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCard(Guid id)
        {
            await _cardService.Delete(id);

            return NoContent();
        }
    }
}