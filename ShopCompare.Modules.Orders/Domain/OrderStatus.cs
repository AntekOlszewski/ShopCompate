namespace ShopCompare.Modules.Orders.Domain;

public enum OrderStatus
{
    PendingPayment = 0,
    Paid = 1,
    PaymentFailed = 2,
    Cancelled = 3
}