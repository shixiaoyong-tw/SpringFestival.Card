using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SpringFestival.Card.Entity;
using SpringFestival.Card.Storage;
using SpringFestival.Card.UICommand;
using SpringFestival.Card.ViewModel;

namespace SpringFestival.Card.Service.Implements
{
    public class AudienceService : IAudienceService
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;
        private readonly IAudienceRepository _audienceRepository;

        public AudienceService(
            IMapper mapper,
            ICardRepository cardRepository,
            IAudienceRepository audienceRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
            _audienceRepository = audienceRepository;
        }

        public async Task Vote(AudienceVoteUICommand command)
        {
            var audienceForVote = _mapper.Map<Audience>(command);
            var card = _cardRepository.Get(command.CardId);
            if (card == null)
            {
                throw new Exception("the card does not exist！");
            }

            var audience = await _audienceRepository.Get(audienceForVote.Id);
            audienceForVote.Time = audience?.Time + 1 ?? 1;

            if (audienceForVote.Time > 3)
            {
                throw new Exception("everyone can vote three times at most！");
            }

            if (audience == null)
                await _audienceRepository.Add(audienceForVote);
            else
                await _audienceRepository.Edit(audienceForVote);
        }

        public async Task<List<AudienceLotteryViewModel>> Lottery()
        {
            var cards = await _cardRepository.GetAll();
            var cardsViewModel = _mapper.Map<List<CardVoteViewModel>>(cards);

            var audiences = await _audienceRepository.GetAll();
            cardsViewModel.ForEach(item =>
            {
                item.Time = audiences.Where(x => x.CardId.ToString() == item.CardId)
                    .Sum(x => x.Time).ToString();
            });

            var topThreeCards = cardsViewModel
                .OrderByDescending(x => x.Time)
                .Take(3)
                .ToList();

            var topThreeAudiences = audiences.Where(x =>
                    topThreeCards.Select(c => c.CardId)
                        .Contains(x.CardId.ToString()))
                .ToList();

            var randomAudiences = topThreeAudiences.OrderBy(u => Guid.NewGuid()).Take(3);

            return _mapper.Map<List<AudienceLotteryViewModel>>(randomAudiences);
        }
    }
}