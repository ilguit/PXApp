using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.DependencyInjection;
using PXApp.API.Contracts.Request;
using PXApp.API.Mapping;
using PXApp.Common.Contracts;

namespace PXApp.API;

public static class ConfigureMapping
{
    internal static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddExpressionMapping();

            config.AddMaps(typeof(MessageProfile));

            config.CreateMap<IRequestBodyDto, IHasDateCreated>()
                .ForMember(x => x.DateCreated, o => o.Ignore())
                .IncludeAllDerived();

            config.CreateMap<IRequestBodyDto, IHasId>()
                .ForMember(x => x.Id, o => o.Ignore())
                .IncludeAllDerived();

        });

        services.AddScoped<IServiceMapper, ServiceMapper>();

        return services;
        
    }

    internal static void CheckMapping(this IServiceProvider serviceProvider)
    {
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}