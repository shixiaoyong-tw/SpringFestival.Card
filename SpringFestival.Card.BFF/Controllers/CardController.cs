using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpringFestival.Card.Common;
using SpringFestival.Card.Common.Enums;
using SpringFestival.Card.UICommand;
using SpringFestival.Card.ViewModel;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SpringFestival.Card.BFF.Controllers
{
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public List<CardViewModel> PullRequests { get; private set; }

        public CardController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<CardViewModel>>> GetCards()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "api/cards");

            var client = _clientFactory.CreateClient("spring.festival.card.api");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                var cards = await JsonSerializer.DeserializeAsync
                    <List<CardViewModel>>(responseStream,
                        new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

                return Ok(cards);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task PostCard(CardAddUICommand command)
        {
            var client = _clientFactory.CreateClient("spring.festival.card.api");

            var cardJson = new FormUrlEncodedContent(command.GetKeyValuePairs());

            using var httpResponse = await client.PostAsync($"/api/cards", cardJson);

            httpResponse.EnsureSuccessStatusCode();
        }

        [HttpPut]
        public async Task PutCard(CardEditUICommand command)
        {
            var client = _clientFactory.CreateClient("spring.festival.card.api");

            var cardJson = new FormUrlEncodedContent(command.GetKeyValuePairs());

            using var httpResponse =
                await client.PutAsync($"/api/cards", cardJson);

            httpResponse.EnsureSuccessStatusCode();
        }

        [HttpDelete("{id}")]
        public async Task DeleteCard(Guid id)
        {
            var client = _clientFactory.CreateClient("spring.festival.card.api");

            using var httpResponse =
                await client.DeleteAsync($"/api/cards/{id}");

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}