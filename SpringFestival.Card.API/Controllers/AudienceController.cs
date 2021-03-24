using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpringFestival.Card.Service;
using SpringFestival.Card.UICommand;

namespace SpringFestival.Card.API.Controllers
{
    [Route("api/audiences")]
    public class AudienceController : ControllerBase
    {
        private readonly IAudienceService _audienceService;

        public AudienceController(IAudienceService audienceService)
        {
            _audienceService = audienceService;
        }

        [HttpPost]
        public async Task<ActionResult> VoteCard(AudienceVoteUICommand command)
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

            await _audienceService.Vote(command);

            return NoContent();
        }
    }
}