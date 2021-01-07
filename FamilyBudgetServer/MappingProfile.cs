using AutoMapper;
using Entities.Models;
using Entities.Models.DataTransferObjects;

namespace FamilyBudgetServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountType, AccountTypesDTO>();
            CreateMap<Account, AccountDTO>();
        }
    }
}
