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

        return services;
    }
}
