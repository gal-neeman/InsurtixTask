using InsurtixTask.Application.Interfaces;
using InsurtixTask.Application.Mappings;
using InsurtixTask.Application.Services;
using InsurtixTask.Infrastructure.DAOs;
using InsurtixTask.Infrastructure.Options;

namespace InsurtixTask.API.Extensions;

public static class BuilderExtensions
{
    public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<XmlDbOptions>(configuration.GetSection(XmlDbOptions.SectionName));

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBookDao, BookDao>();
        services.AddScoped<IBookService, BookService>();

        services.AddAutoMapper(typeof(BookProfile));

        return services;
    }
}
