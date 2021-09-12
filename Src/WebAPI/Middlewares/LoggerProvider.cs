using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class LoggerProvider
    {
        private readonly RequestDelegate next;

        public LoggerProvider(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            return next(context);
        }
    }
}
