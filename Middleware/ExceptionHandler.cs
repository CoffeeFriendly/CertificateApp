using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CertificatesApp.Middleware
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context, 
            Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Произошла ошибка: {message}", exception.Message);

            // ЗАМЕНИТЬ после создания кастомных исключений!!!
            var (statusCode, title) = exception switch
            {
                Exception => (StatusCodes.Status403Forbidden, "Error placeholder")
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
