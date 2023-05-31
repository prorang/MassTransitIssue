using CommandExecutor;
using MassTransit;

namespace MassTransitTutorials;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitServices(this IServiceCollection services)
    {
        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;
            });
        
        services.AddMassTransit(x =>
        {
            x.AddConfigureEndpointsCallback((name, cfg) =>
            {
                cfg.UseMessageRetry(r =>
                {
                    r.Immediate(3);
                    r.Intervals(100, 500, 1000);
                });
            });
            
            x.AddConsumer<CheckOrderStatusConsumer>()
                .Endpoint(e => e.Name = "order-status");
        
            x.AddRequestClient<ExecuteCommand>(new Uri("exchange:order-status"));
    
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ClearSerialization(); 
                cfg.UseNewtonsoftRawJsonSerializer();
                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}