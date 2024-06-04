using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace JwtAuthMvcApp.Middleware
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("jwtToken"))
            {
                var token = context.Request.Cookies["jwtToken"];
                context.Request.Headers.Add("Authorization", $"Bearer {token}");
            }

            await _next(context);
        }
    }
}
