namespace ShopCompare.SharedKernel.Exceptions;

public sealed class DomainException(string message) : Exception(message);