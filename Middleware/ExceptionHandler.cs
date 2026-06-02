using CertificatesApp.Exceptions;
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

            var (statusCode, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Заявка не найдена"),
                InvalidStatusChangeException => (StatusCodes.Status400BadRequest, "Некорректное изменение статуса заявки"),
                DuplicateRequestException => (StatusCodes.Status409Conflict, "Попытка создать дубликат заявки"),
                InvalidEmployeeException => (StatusCodes.Status404NotFound, "Сотрудник не найден"),
                _ => (StatusCodes.Status500InternalServerError, "Внутренняя ошибка")
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
