using AutoMapper;
using ECommerce.Business.ViewModels.FruitVMs;
using ECommerce.Business.ViewModels.UserVMs;
using ECommerce.Core.Entities;

namespace ECommerce.MVC
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateFruitVm, Fruit>().ReverseMap();
            CreateMap<UpdateFruitVm, Fruit>().ReverseMap();
            CreateMap<RegisterUserVm, AppUser>().ReverseMap();
            CreateMap<LoginUserVm, AppUser>().ReverseMap();
        }
    }
}
