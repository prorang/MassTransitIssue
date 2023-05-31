using MassTransit;
using Microsoft.Extensions.Logging;

namespace CommandExecutor;

public class CheckOrderStatusConsumer : 
    IConsumer<ExecuteCommand>
{
    private readonly ILogger<CheckOrderStatusConsumer> _logger;

    public CheckOrderStatusConsumer(ILogger<CheckOrderStatusConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ExecuteCommand> context)
    {
        _logger.LogInformation("start");

        await context.RespondAsync<ExecuteResponse>(new
        {
            Success = true
        });
        
        _logger.LogInformation("finish");
    }
}