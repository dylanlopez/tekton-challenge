using Serilog;
using System.Diagnostics;

namespace Tekton.WebApi.Middleware
{
	public class RequestLoggingMiddleware
	{
		private readonly RequestDelegate _next;

		public RequestLoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var watch = Stopwatch.StartNew();
			try
			{
				await _next(context);
			}
			finally
			{
				watch.Stop();
				var logMessage = $"Request [{context.Request.Method}] at {context.Request.Path} took {watch.ElapsedMilliseconds} ms";
				Log.Information(logMessage);
			}
		}
	}
}
