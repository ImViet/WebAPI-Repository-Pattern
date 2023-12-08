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
                        errorResult.Message = e.Message;
                        break;
                    case BadRequestException e:
                        errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResult.Message = e.Message;
                        break;
                    case ErrorException e:
                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResult.Message = e.Message;
                        break;
                    default:
                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResult.Message = "Something went wrong!!!";
                        break;
                }
                var result = JsonConvert.SerializeObject(errorResult);
                await response.WriteAsync(result);
            }
        }
    }
}
