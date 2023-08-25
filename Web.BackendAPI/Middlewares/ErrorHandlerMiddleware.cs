using Newtonsoft.Json;
using System.Net;
using Web.Contracts.Exceptions;

namespace Web.BackendAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var errorResult = new ErrorResultModel();
                var response = context.Response;
                response.ContentType = "application/json";
                errorResult.Message = error.Message ?? "Something went wrong!!!";
                switch (error)
                {
                    case NotFoundException e:
                        errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ErrorException e:
                        errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonConvert.SerializeObject(errorResult);
                await response.WriteAsync(result);
            }
        }
    }
}
