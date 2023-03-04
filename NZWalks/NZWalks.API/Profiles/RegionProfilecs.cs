using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionProfilecs : Profile
    {

        public RegionProfilecs()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
        }
    }
}
