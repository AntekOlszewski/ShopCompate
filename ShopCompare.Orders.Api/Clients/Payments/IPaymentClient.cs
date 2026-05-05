namespace ShopCompare.Orders.Api.Clients.Payments;

public interface IPaymentsClient
{
    Task<ProcessPaymentResponse> ProcessPaymentAsync(
        ProcessPaymentRequest request,
        CancellationToken cancellationToken = default);
}