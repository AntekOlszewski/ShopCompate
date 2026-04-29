namespace ShopCompare.Modules.Payments.PublicApi;

public interface IPaymentModule
{
    Task<ProcessPaymentResult> ProcessPaymentAsync(
        Guid orderId,
        decimal amount,
        CancellationToken cancellationToken = default);
}