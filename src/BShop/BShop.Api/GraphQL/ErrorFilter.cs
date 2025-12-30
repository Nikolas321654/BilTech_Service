using BShop.Domain.CustomExceptions;

namespace BShop.GraphQL;

public abstract class ErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is null) return error;

        return error.Exception switch
        {
            NotFoundException ex =>
                error.WithMessage(ex.Message)
                    .WithCode("NOT_FOUND")
                    .SetExtension("statusCode", 404),

            BadRequestException ex =>
                error.WithMessage(ex.Message)
                    .WithCode("BAD_REQUEST")
                    .SetExtension("statusCode", 400),

            AlreadyAddedException ex =>
                error.WithMessage(ex.Message)
                    .WithCode("ALREADY_ADDED")
                    .SetExtension("statusCode", 409),

            _ => error.WithMessage("An unexpected server error occurred.")
                .WithCode("INTERNAL_SERVER_ERROR")
        };
    }
}