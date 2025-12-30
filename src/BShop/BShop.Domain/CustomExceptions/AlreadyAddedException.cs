namespace BShop.Domain.CustomExceptions;

public class AlreadyAddedException(string message) : Exception(message);