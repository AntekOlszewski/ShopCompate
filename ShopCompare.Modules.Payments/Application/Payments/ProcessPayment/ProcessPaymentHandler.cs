using ShopCompare.Modules.Payments.Domain;
using ShopCompare.Modules.Payments.Infrastructure.Persistence;

namespace ShopCompare.Modules.Payments.Application.Payments.ProcessPayment;

public sealed class ProcessPaymentHandler(PaymentsDbContext dbContext)
{
    private readonly Random _random = new();

    public async Task<ProcessPaymentResponse> HandleAsync(
        ProcessPaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        var payment = new Payment(
            Guid.NewGuid(),
            request.OrderId,
            request.Amount);

        dbContext.Payments.Add(payment);

        var delay = _random.Next(200, 801);

        await Task.Delay(delay, cancellationToken);

        //var isSuccessful = _random.NextDouble() <= 0.95;
        var isSuccessful = true;

        if (isSuccessful)
        {
            payment.MarkAsSucceeded();
        }
        else
        {
            payment.MarkAsFailed("Simulated payment provider failure.");
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ProcessPaymentResponse(
            payment.Status == PaymentStatus.Succeeded,
            payment.Id,
            payment.FailureReason);
    }
}