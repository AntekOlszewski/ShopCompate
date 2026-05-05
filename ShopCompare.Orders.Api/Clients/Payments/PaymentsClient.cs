namespace ShopCompare.Orders.Api.Clients.Payments;

internal sealed class PaymentsClient(HttpClient httpClient) : IPaymentsClient
{
    public async Task<ProcessPaymentResponse> ProcessPaymentAsync(
        ProcessPaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            "/internal/payments",
            request,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ProcessPaymentResponse>(
                cancellationToken);

            return error ?? new ProcessPaymentResponse(false, null, "Payment failed.");
        }

        var result = await response.Content.ReadFromJsonAsync<ProcessPaymentResponse>(
            cancellationToken);

        return result ?? new ProcessPaymentResponse(false, null, "Payments returned empty response.");
    }
}