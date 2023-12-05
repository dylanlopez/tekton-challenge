using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace Tekton.Application
{
	public static class ApplicationExtensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(Assembly.GetExecutingAssembly());
			return services;
		}
	}
}
