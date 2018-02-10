using AutoMapper;
using Budgie.Core;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, ApiCategory>();
        CreateMap<Outgoing, ApiOutgoing>();
        CreateMap<Income, ApiIncome>();
        CreateMap<Saving, ApiSaving>();

        CreateMap<Budget, ApiBudget>()
        .ForMember(dest => dest.TotalBudgeted, src => src.MapFrom(x => x.Outgoings.Sum(y => y.Budgeted)))
        .ForMember(dest => dest.TotalSaved, src => src.MapFrom(x => x.Savings.Sum(y => y.Total)))
        .ForMember(dest => dest.IncomeVsExpenditure, src => src.MapFrom(x => GetIncomeVsExpenditure(x)));

        CreateMap<Transaction, ApiTransaction>();
    }

    private decimal GetIncomeVsExpenditure(Budget budget)
    {
        var totalIncome = budget.Incomes.Sum(x => x.Total);
        var totalExpenditure = budget.Outgoings.Sum(x => x.Actual);

        return totalIncome - totalExpenditure;
    }
}