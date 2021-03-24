using System.Collections.Generic;
using System.Threading.Tasks;
using SpringFestival.Card.UICommand;
using SpringFestival.Card.ViewModel;

namespace SpringFestival.Card.Service
{
    public interface IAudienceService
    {
        /// <summary>
        /// 观众投票
        /// </summary>
        Task Vote(AudienceVoteUICommand command);

        /// <summary>
        /// 抽奖
        /// </summary>
        Task<List<AudienceLotteryViewModel>> Lottery();
    }
}