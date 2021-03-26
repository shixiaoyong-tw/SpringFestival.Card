using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SpringFestival.Card.Service;

namespace SpringFestival.Card.Test
{
    public static class AutoMap
    {
        public static IMapper Get()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            var scope = services.BuildServiceProvider().CreateScope();
            var _mapper = scope.ServiceProvider.GetService<IMapper>();
            return _mapper;
        }
    }
}