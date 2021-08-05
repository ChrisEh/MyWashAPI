using AutoMapper;
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
            CreateMap<PickupWriteDto, Pickup>();
        }
    }
}
