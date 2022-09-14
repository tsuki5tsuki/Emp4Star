using Domain.Models.Responses;
using System.Net;
using System.Text.Json;

namespace Emp4Star.Middlewares
{
  public class CustomExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
      try
      {
        await _next(httpContext);
      }
      catch (Exception ex)
      {
        _logger.LogError("Unhandled exception ...", ex);
        await HandleExceptionAsync(httpContext, ex);
      }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      context.Response.ContentType = "application/json";
      var response = context.Response;

      var errorResponse = new ErrorResponse();
      switch (exception)
      {
        case ApplicationException ex:
          if (ex.Message.Contains("Invalid Token"))
          {
            errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.Forbidden;
            errorResponse.Message = ex.Message;
            break;
          }
          else if (ex.Message.Contains("Data not found") || ex.Message.Contains("No result"))
          {
            errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.NotFound;
            errorResponse.Message = ex.Message;
            break;
          }
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.BadRequest;
          errorResponse.Message = ex.Message;
          break;
        case BadHttpRequestException ex:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.BadRequest;
          errorResponse.Message = ex.Message;
          break;
        case UnauthorizedAccessException ex:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.Unauthorized;
          errorResponse.Message = ex.Message;
          break;
        case FileNotFoundException ex:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.NotFound;
          errorResponse.Message = ex.Message;
          break;
        case KeyNotFoundException ex:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.NotFound;
          errorResponse.Message = ex.Message;
          break;
        case InvalidDataException ex:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.NotFound;
          errorResponse.Message = ex.Message;
          break;
        case MethodAccessException ex:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
          errorResponse.Message = ex.Message;
          break;
        case TimeoutException ex:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.RequestTimeout;
          errorResponse.Message = ex.Message;
          break;
        case NotImplementedException ex:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.NotImplemented;
          errorResponse.Message = ex.Message;
          break;
        case WebException ex:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.BadGateway;
          errorResponse.Message = ex.Message;
          break;
        default:
          errorResponse.StatusCode = response.StatusCode = (int)HttpStatusCode.InternalServerError;
          errorResponse.Message = "Internal server error. Please contact the administrator.";
          break;
      }
      _logger.LogError(exception.Message);
      var result = JsonSerializer.Serialize(errorResponse);
      await context.Response.WriteAsync(result);
    }
  }
}
