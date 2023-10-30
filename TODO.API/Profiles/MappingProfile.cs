using AutoMapper;
using TODO.API.DTOs;
using TODO.API.Models;

namespace TODO.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
               CreateMap<ItemData , GetItemDataDto>().ReverseMap();

               CreateMap<ItemData , CreateItemDataDto>().ReverseMap();

               CreateMap<ItemData , UpdateItemDataDto>().ReverseMap();  

        }
    }
}
