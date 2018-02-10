using AutoMapper;
using Budgie.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, ApiCategory>();
        CreateMap<Budget, ApiBudget>();
        CreateMap<Outgoing, ApiOutgoing>();
        CreateMap<Income, ApiIncome>();
        CreateMap<Saving, ApiSaving>();
        CreateMap<Transaction, ApiTransaction>();
    }
}