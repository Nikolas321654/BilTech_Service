namespace BShop.Domain.CustomExepions;

public class BadRequestException(string message) : Exception(message);