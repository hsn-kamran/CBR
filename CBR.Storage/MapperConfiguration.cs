using AutoMapper;
using CBR.Data.Models;
using CBR.Storage.Models;

namespace CBR.Storage;

public class MapperConfig
{
    public static Mapper InitializeAutomapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Item, Currency>()
            .ForMember(d => d.CurrencyId, opt => opt.MapFrom(s => s.Id));

            cfg.CreateMap<Valute, CurrencyCourse>()
                .ForMember(d => d.CurrencyId, opt => opt.MapFrom(s => s.Id));
        });
        
        var mapper = new Mapper(config);

        return mapper;
    }
}
