namespace Crayon.Infrastructure.Common
{
    public record Result<T>(bool IsSuccess, T? Data, int StatusCode, string? ErrorMessage)
    {
        public static Result<T> Success(T data, int statusCode = 200) =>
            new(true, data, statusCode, null);

        public static Result<T> Failure(string errorMessage, int statusCode) =>
            new(false, default, statusCode, errorMessage);
    }
}
