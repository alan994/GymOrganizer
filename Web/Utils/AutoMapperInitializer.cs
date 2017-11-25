using AutoMapper;
using BusinessLogic.Model;
using Data.Model;
using Web.ViewModels;

namespace Web.Utils
{
    public class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {

                #region Office
                cfg.CreateMap<OfficeVM, Office>().ReverseMap();
                cfg.CreateMap<OfficeVM, OfficeQueue>()
                .ForMember(dest => dest.CityId, options => options.MapFrom(x => x.City.Id))
                .ForMember(dest => dest.UserPerformingAction, options => options.Ignore())
                .ForMember(dest => dest.TenantId, options => options.Ignore())
                .ReverseMap();
                #endregion

                #region City
                cfg.CreateMap<CityVM, City>().ReverseMap();
                cfg.CreateMap<CityVM, CityQueue>()
                .ForMember(dest => dest.CountryId, options => options.MapFrom(x => x.Country.Id))
                .ForMember(dest => dest.UserPerformingAction, options => options.Ignore())
                .ForMember(dest => dest.TenantId, options => options.Ignore())
                .ReverseMap();
                #endregion

                #region Country
                cfg.CreateMap<CountryVM, Country>().ReverseMap();                
                cfg.CreateMap<CountryVM, CountryQueue>()                
                .ForMember(dest => dest.UserPerformingAction, options => options.Ignore())
                .ForMember(dest => dest.TenantId, options => options.Ignore())
                .ReverseMap();
                #endregion

                #region Tenant
                cfg.CreateMap<TenantVM, Tenant>().ReverseMap();
                cfg.CreateMap<TenantVM, TenantQueue>()
                .ForMember(dest => dest.UserPerformingAction, options => options.Ignore())
                .ForMember(dest => dest.TenantId, options => options.Ignore())
                .ReverseMap();
                #endregion

                #region Term
                cfg.CreateMap<TermVM, Term>().ReverseMap();
                cfg.CreateMap<TermVM, TermQueue>()
                .ForMember(dest => dest.UserPerformingAction, options => options.Ignore())
                .ForMember(dest => dest.TenantId, options => options.Ignore())
                .ReverseMap();
                #endregion

                #region User
                cfg.CreateMap<UserVM, User>().ReverseMap();
                cfg.CreateMap<UserVM, UserQueue>()
                .ForMember(dest => dest.UserPerformingAction, options => options.Ignore())
                .ForMember(dest => dest.TenantId, options => options.Ignore())
                .ReverseMap();
                #endregion
            });
        }
    }
}
