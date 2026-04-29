using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopCompare.Modules.Payments.Application;
using ShopCompare.Modules.Payments.Application.Payments.ProcessPayment;
using ShopCompare.Modules.Payments.Infrastructure.Persistence;
using ShopCompare.Modules.Payments.PublicApi;

namespace ShopCompare.Modules.Payments;

public static class DependencyInjection
{
    public static IServiceCollection AddPaymentsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PaymentsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("shopcompare-db"));
        });

        services.AddScoped<IPaymentModule, PaymentModule>();

        services.AddScoped<ProcessPaymentHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}