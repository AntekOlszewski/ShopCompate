using ShopCompare.Modules.Payments.Application.Payments.ProcessPayment;
using ShopCompare.Modules.Payments.PublicApi;

namespace ShopCompare.Modules.Payments.Application;

internal sealed class PaymentModule : IPaymentModule
{
    private readonly ProcessPaymentHandler _handler;

    public PaymentModule(ProcessPaymentHandler handler)
    {
        _handler = handler;
    }

    public async Task<ProcessPaymentResult> ProcessPaymentAsync(
        Guid orderId,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        var request = new ProcessPaymentRequest(orderId, amount);

        var response = await _handler.HandleAsync(request, cancellationToken);

        return new ProcessPaymentResult(
            response.Success,
            response.PaymentId,
            response.Error);
    }
}