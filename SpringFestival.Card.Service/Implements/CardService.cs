using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SpringFestival.Card.Storage;
using SpringFestival.Card.UICommand;
using SpringFestival.Card.ViewModel;

namespace SpringFestival.Card.Service.Implements
{
    public class CardService : ICardService
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;
        private readonly IAudienceRepository _audienceRepository;

        public CardService(
            IMapper mapper,
            ICardRepository cardRepository,
            IAudienceRepository audienceRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
            _audienceRepository = audienceRepository;
        }

        public async Task<List<CardViewModel>> GetAll()
        {
            var cards = await _cardRepository.GetAll();
            var cardsView = _mapper.Map<List<CardViewModel>>(cards);

            return cardsView;
        }

        public async Task<CardViewModel> Get(Guid id)
        {
            var card = await _cardRepository.Get(id);
            var cardView = _mapper.Map<CardViewModel>(card);

            return cardView;
        }

        public async Task Add(CardAddUICommand command)
        {
            var cardForAdd = _mapper.Map<Entity.Card>(command);

            var card = await _cardRepository.Get(cardForAdd.Id);

            if (card != null)
            {
                throw new Exception("card has exist!");
            }

            await _cardRepository.Add(cardForAdd);
        }

        public async Task Edit(CardEditUICommand command)
        {
            var cardForEdit = _mapper.Map<Entity.Card>(command);

            // var card = await _cardRepository.Get(cardForEdit.Id);
            //
            // if (card == null)
            // {
            //     throw new Exception("card is not exist!");
            // }

            await _cardRepository.Edit(cardForEdit);
        }

        public async Task Delete(CardDeleteUICommand command)
        {
            var card = await _cardRepository.Get(command.Id);

            // if (card == null)
            // {
            //     throw new Exception("card is not exist!");
            // }

            await _cardRepository.Delete(command.Id);
        }

        public async Task<List<CardVoteViewModel>> GetVoteResult()
        {
            var cards = await _cardRepository.GetAll();
            var cardsViewModel = _mapper.Map<List<CardVoteViewModel>>(cards);

            var audiences = await _audienceRepository.GetAll();
            cardsViewModel.ForEach(item =>
            {
                item.Time = audiences.Where(x => x.CardId.ToString() == item.CardId)
                    .Sum(x => x.Time).ToString();
            });

            return cardsViewModel;
        }
    }
}