using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpringFestival.Card.Common;
using SpringFestival.Card.UICommand;
using SpringFestival.Card.ViewModel;

namespace SpringFestival.Card.BFF.Controllers
{
    [Route("api/audiences")]
    public class AudienceController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public AudienceController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("lotteries")]
        public async Task<ActionResult<List<AudienceLotteryViewModel>>> GetLotteryResult()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "api/audiences/lotteries");

            var client = _clientFactory.CreateClient("spring.festival.card.api");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                var audienceLotteries = await JsonSerializer.DeserializeAsync
                    <List<AudienceLotteryViewModel>>(responseStream,
                        new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

                return Ok(audienceLotteries);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task VoteCard(AudienceVoteUICommand command)
        {
            var client = _clientFactory.CreateClient("spring.festival.card.api");

            var voteJson = new FormUrlEncodedContent(command.GetKeyValuePairs());

            using var httpResponse = await client.PostAsync($"/api/audiences", voteJson);

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}