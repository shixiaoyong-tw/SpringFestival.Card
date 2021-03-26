using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpringFestival.Card.UICommand;
using SpringFestival.Card.ViewModel;

namespace SpringFestival.Card.Service
{
    public interface ICardService
    {
        Task<List<CardViewModel>> GetAll();

        Task<CardViewModel> Get(Guid id);

        Task Add(CardAddUICommand command);

        Task Edit(CardEditUICommand command);

        Task Delete(CardDeleteUICommand command);

        /// <summary>
        /// 获取投票结果
        /// </summary>
        Task<List<CardVoteViewModel>> GetVoteResult();
    }
}