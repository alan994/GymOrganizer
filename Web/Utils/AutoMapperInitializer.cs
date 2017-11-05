using AutoMapper;
using BusinessLogic.Model;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Utils
{
    public class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<City, CityVM>().ReverseMap();
                cfg.CreateMap<Country, CountryVM>().ReverseMap();

                #region Office
                cfg.CreateMap<Office, OfficeVM>().ReverseMap();
                cfg.CreateMap<OfficeVM, OfficeQueue>()
                .ForMember(dest => dest.CityId, options => options.MapFrom(x => x.City.Id))
                .ForMember(dest => dest.UserPerformingAction, options => options.Ignore())
                .ForMember(dest => dest.TenantId, options => options.Ignore())
                .ReverseMap();
                #endregion
            });
        }
    }
}
