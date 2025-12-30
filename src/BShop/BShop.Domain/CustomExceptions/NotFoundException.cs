namespace BShop.Domain.CustomExceptions;

public class NotFoundException(string message) : Exception(message);