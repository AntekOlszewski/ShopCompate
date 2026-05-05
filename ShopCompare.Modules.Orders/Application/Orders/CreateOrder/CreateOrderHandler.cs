using ShopCompare.Cart.Api.PublicApi;
using ShopCompare.Inventory.Api.PublicApi;
using ShopCompare.Notifications.Api.PublicApi;
using ShopCompare.Modules.Orders.Domain;
using ShopCompare.Modules.Orders.Infrastructure.Persistence;
using ShopCompare.Payments.Api.PublicApi;

namespace ShopCompare.Modules.Orders.Application.Orders.CreateOrder;

public sealed class CreateOrderHandler(
    OrdersDbContext dbContext,
    ICartModule cartModule,
    IInventoryModule inventoryModule,
    IPaymentModule paymentModule,
    INotificationModule notificationModule)
{
    public async Task<CreateOrderResponse> HandleAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var cart = await cartModule.GetCartAsync(
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

        var reserveItems = cart.Items
            .Select(x => new ReserveStockItem(
                x.ProductId,
                x.Quantity))
            .ToList();

        var reservationResult = await inventoryModule.ReserveStockAsync(
            reserveItems,
            orderId,
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

        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        var paymentResult = await paymentModule.ProcessPaymentAsync(
            order.Id,
            order.TotalAmount,
            cancellationToken);

        if (!paymentResult.Success)
        {
            order.MarkPaymentAsFailed(
                paymentResult.Error ?? "Payment failed.");

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResponse(
                false,
                order.Id,
                paymentResult.Error ?? "Payment failed.");
        }

        if (paymentResult.PaymentId is null)
        {
            order.MarkPaymentAsFailed("Payment id was not returned.");

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResponse(
                false,
                order.Id,
                "Payment id was not returned.");
        }

        order.MarkAsPaid(paymentResult.PaymentId.Value);

        await cartModule.ClearCartAsync(
            request.UserId,
            cancellationToken);

        await notificationModule.SendOrderConfirmationAsync(
            request.UserId,
            order.Id,
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResponse(
            true,
            order.Id,
            null);
    }
}