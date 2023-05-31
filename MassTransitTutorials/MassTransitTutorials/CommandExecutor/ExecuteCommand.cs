using MassTransit;

namespace CommandExecutor;

public interface ExecuteCommand
{
    string Name { get; set; }
}