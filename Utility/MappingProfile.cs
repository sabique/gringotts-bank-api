using AutoMapper;
using System;
using static Utility.Constant;
using DM = DomainModel;
using SM = ServiceModel;
namespace Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SM.Customer, DM.Customer>();
            CreateMap<SM.Account, DM.Account>();
            CreateMap<SM.OpenAccount, DM.Account>()
                .ForMember(d => d.CreatedOn, s => s.MapFrom(x => DateTime.UtcNow))
                .ForMember(d => d.ModifiedOn, s => s.MapFrom(x => DateTime.UtcNow))
                .ForMember(d => d.Currency, s => s.MapFrom(x => Currency.USD));

            CreateMap<SM.Transaction, DM.Transaction>()
                .ForMember(d => d.CreatedOn, s => s.MapFrom(x => DateTime.UtcNow));

            CreateMap<DM.Transaction, SM.TransactionDetail>();
            CreateMap<DM.Account, SM.AccountDetail>()
                .ForMember(d => d.Balance, s => s.MapFrom(x => x.Amount))
                .ForMember(d => d.AccountNumber, s => s.MapFrom(x => x.Id));
        }
    }
}
