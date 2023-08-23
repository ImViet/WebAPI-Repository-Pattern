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
                switch (error)
                {
                    case NotFoundException e:
                        errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                        errorResult.Message = error.InnerException?.Message ?? "Something went wrong!!!";
                        break;
                    default:
                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResult.Message = error.InnerException?.Message ?? "Something went wrong!!!";
                        break;
                }
                var result = JsonConvert.SerializeObject(errorResult);
                await response.WriteAsync(result);
            }
        }
    }
}
