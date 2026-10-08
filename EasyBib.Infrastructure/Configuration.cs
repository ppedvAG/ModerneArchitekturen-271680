using EasyBib.Domain.Contracts;
using EasyBib.Domain.Services;
using EasyBib.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;

namespace EasyBib.Infrastructure;

public static class Configuration
{
    public static IServiceCollection AddRepositories(this IServiceCollection collection, string connectionString)
    {
        collection.AddSqlServer<EasyBibDbContext>(connectionString);
        collection.AddScoped<ILoanRepository, LoanRepository>();
        collection.AddScoped<IMemberRepository, MemberRepository>();
        collection.AddScoped<IMediaItemRepository, MediaItemRepository>();
        collection.AddScoped<LoanService>();
        return collection;
    }
}
