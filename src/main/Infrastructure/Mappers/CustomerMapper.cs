using Mapster;
using NttBank.Mcp.Domain.Entities;
using NttBank.Mcp.Infrastructure.Results;

namespace NttBank.Mcp.Infrastructure.Mappers;

public class CustomerMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<CustomerResult, Customer>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.Phone, src => src.Phone)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Occupation, src => src.Occupation)
            .Map(dest => dest.AnnualIncome, src => src.AnnualIncome)
            .Map(dest => dest.JoinDate, src => src.JoinDate)
            .Map(dest => dest.CreditScore, src => src.CreditScore);
    }
}
