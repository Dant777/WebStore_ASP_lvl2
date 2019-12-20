using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebStore.Services
{
    public class ErrorHandlingMiddleware
    {
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(typeof(ErrorHandlingMiddleware));

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
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
                throw;
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            Log.Error(ex.Message, ex);
            return  Task.CompletedTask;
        }
    }
}
