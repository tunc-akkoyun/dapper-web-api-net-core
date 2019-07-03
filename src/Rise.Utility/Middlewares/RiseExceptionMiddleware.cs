using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Rise.Utility.Enums;
using Rise.Utility.Exceptions;
using System;
using System.Threading.Tasks;

namespace Rise.Utility.Middlewares
{
    public class RiseExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public RiseExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var statusCode = ApiStatusCodes.InternalServer;
            var message = exception.Message ?? "Beklenmedik bir hata oluştu";

            if (exception is RiseException rExp)
            {
                message = rExp.Message;
                statusCode = rExp.Code;
            }

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
            {
                Message = message
            }));
        }
    }
}
