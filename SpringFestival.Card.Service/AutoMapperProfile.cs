using AutoMapper;
using SpringFestival.Card.Common.Enums;
using SpringFestival.Card.Entity;
using SpringFestival.Card.UICommand;
using SpringFestival.Card.ViewModel;

namespace SpringFestival.Card.Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CardAddUICommand, Entity.Card>();

            CreateMap<CardEditUICommand, Entity.Card>();

            CreateMap<Entity.Card, CardViewModel>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.CardType,
                    opt =>
                        opt.MapFrom(src => src.CardType.GetDescription()));

            CreateMap<AudienceVoteUICommand, Audience>();

            CreateMap<Entity.Card, CardVoteViewModel>()
                .ForMember(dest => dest.CardId,
                    opt =>
                        opt.MapFrom(src => src.Id.ToString()));

            CreateMap<Audience, AudienceLotteryViewModel>();
        }
    }
}