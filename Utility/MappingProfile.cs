using AutoMapper;
using DM = DomainModel;
using SM = ServiceModel;
namespace Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SM.Customer, DM.Customer>();
        }
    }
}
