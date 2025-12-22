namespace BShop.Domain.CustomExepions;

public class AlreadyAddedException(string message) : Exception(message);