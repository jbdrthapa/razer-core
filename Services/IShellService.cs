namespace RazerCore.Services;

public interface IShellService : IDisposable
{
    string? ExecuteCommand(string command, string[]? arguments = null);
}