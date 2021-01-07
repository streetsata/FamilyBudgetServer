using AutoMapper;
using Entities.Models;
using Entities.Models.DataTransferObjects;

namespace FamilyBudgetServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region AccountType
            CreateMap<AccountType, AccountTypesDTO>();
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountTypeForCreationDto, AccountType>();
            CreateMap<AccountTypeForUpdateDTO, AccountType>();
            #endregion
        }
    }
}
