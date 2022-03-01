using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HotelManager.Middleware
{
    public class HandlerExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HandlerExceptionMiddleware(RequestDelegate next)
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
                var response = context.Response;
                response.ContentType = "application/json";
                string errorCode = Guid.NewGuid().ToString();
                var message = "";
                switch (ex)
                {
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        message = "No se encontraron resultados";
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = "Ocurrio un error interno.";
                        break;
                }
                Log.ForContext("errorCode", errorCode).Fatal(ex.Message, ex);

                var result = JsonConvert.SerializeObject(new { message = message, errorcode = errorCode });
                await response.WriteAsync(result);
            }
        }

    }
}
