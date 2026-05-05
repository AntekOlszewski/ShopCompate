using ShopCompare.Orders.Api.Clients;
using ShopCompare.Orders.Api.Clients.Cart;
using ShopCompare.Orders.Api.Clients.Inventory;
using ShopCompare.Orders.Api.Clients.Notifications;
using ShopCompare.Orders.Api.Clients.Payments;
using ShopCompare.Orders.Api.Domain;
using ShopCompare.Orders.Api.Infrastructure.Persistence;

namespace ShopCompare.Orders.Api.Application.Orders.CreateOrder;

public sealed class CreateOrderHandler
{
    private readonly OrdersDbContext _dbContext;
    private readonly ICartClient _cartClient;
    private readonly IInventoryClient _inventoryClient;
    private readonly IPaymentsClient _paymentsClient;
    private readonly INotificationsClient _notificationsClient;

    public CreateOrderHandler(
        OrdersDbContext dbContext,
        ICartClient cartClient,
        IInventoryClient inventoryClient,
        IPaymentsClient paymentsClient,
        INotificationsClient notificationsClient)
    {
        _dbContext = dbContext;
        _cartClient = cartClient;
        _inventoryClient = inventoryClient;
        _paymentsClient = paymentsClient;
        _notificationsClient = notificationsClient;
    }

    public async Task<CreateOrderResponse> HandleAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var cart = await _cartClient.GetCartAsync(
            request.UserId,
            cancellationToken);

        if (cart is null || cart.Items.Count == 0)
        {
            return new CreateOrderResponse(
                false,
                null,
                "Cart is empty.");
        }

        var orderId = Guid.NewGuid();

        var reservationResult = await _inventoryClient.ReserveStockAsync(
            new ReserveStockRequest(
                orderId,
                cart.Items.Select(x => new ReserveStockRequestItem(
                    x.ProductId,
                    x.Quantity)).ToList()),
            cancellationToken);

        if (!reservationResult.Success)
        {
            return new CreateOrderResponse(
                false,
                null,
                reservationResult.Error);
        }

        var orderItems = cart.Items
            .Select(x => new OrderItem(
                Guid.NewGuid(),
                orderId,
                x.ProductId,
                x.ProductName,
                x.UnitPrice,
                x.Quantity))
            .ToList();

        var order = new Order(
            orderId,
            request.UserId,
            orderItems);

        _dbContext.Orders.Add(order);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var paymentResult = await _paymentsClient.ProcessPaymentAsync(
            new ProcessPaymentRequest(
                order.Id,
                order.TotalAmount),
            cancellationToken);

        if (!paymentResult.Success)
        {
            order.MarkPaymentAsFailed(
                paymentResult.Error ?? "Payment failed.");

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResponse(
                false,
                order.Id,
                paymentResult.Error ?? "Payment failed.");
        }

        if (paymentResult.PaymentId is null)
        {
            order.MarkPaymentAsFailed("Payment id was not returned.");

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResponse(
                false,
                order.Id,
                "Payment id was not returned.");
        }

        order.MarkAsPaid(paymentResult.PaymentId.Value);

        await _cartClient.ClearCartAsync(
            request.UserId,
            cancellationToken);

        await _notificationsClient.QueueOrderConfirmationAsync(
            new QueueOrderConfirmationRequest(
                request.UserId,
                order.Id),
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResponse(
            true,
            order.Id,
            null);
    }
}