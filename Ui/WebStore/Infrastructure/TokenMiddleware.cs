using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebStore.Infrastructure
{
    internal class TokenMiddleware
    {
        private const string correctToken = "qwerty123";

        public RequestDelegate Next { get; }

        public TokenMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["referenceToken"];

            if (string.IsNullOrEmpty(token))
            {
                await Next.Invoke(context);
            }

            if (token == correctToken)
            {
                // обрабатываем токен...
                await Next.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("Token is incorrect");
            }
        }
    }
}