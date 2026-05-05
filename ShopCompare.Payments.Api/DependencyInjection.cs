using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopCompare.Payments.Api.Application.Payments.ProcessPayment;
using ShopCompare.Payments.Api.Infrastructure.Persistence;

namespace ShopCompare.Payments.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPaymentsApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PaymentsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("payments-db"));
        });

        services.AddScoped<ProcessPaymentHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}