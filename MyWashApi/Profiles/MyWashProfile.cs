using AutoMapper;
using System.Linq;
using MyWashApi.Data.Models;
using MyWashApi.Dtos;

namespace MyWashApi.Profiles
{
    public class MyWashProfile : Profile
    {
        public MyWashProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserCreateDto, User>();
            CreateMap<ShoppingCartItemsDto, ShoppingCart>()
                .ForMember(
                    dst => dst.Products,
                    options => options.MapFrom(
                        src => src.ProductIds.ToList()));
        }
    }
}
