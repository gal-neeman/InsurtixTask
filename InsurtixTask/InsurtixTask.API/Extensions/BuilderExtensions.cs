using FluentValidation;
using InsurtixTask.Application.Interfaces;
using InsurtixTask.Application.Mappings;
using InsurtixTask.Application.Options;
using InsurtixTask.Application.Services;
using InsurtixTask.Application.Validators;
using InsurtixTask.Infrastructure.DAOs;
using InsurtixTask.Infrastructure.Options;
using InsurtixTask.Infrastructure.Services;

namespace InsurtixTask.API.Extensions;

public static class BuilderExtensions
{
    public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<XmlDbOptions>(configuration.GetSection(XmlDbOptions.SectionName));
        services.Configure<ValidationOptions>(configuration.GetSection(ValidationOptions.SectionName));

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookDao, BookDao>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IReportService, ReportService>();

        services.AddAutoMapper(typeof(BookProfile));

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<BookRequestValidator>();

        return services;
    }
}
